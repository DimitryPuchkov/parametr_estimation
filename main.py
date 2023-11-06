import random
import numpy as np
import matplotlib.pyplot as plt
import pandas as pd
from dataclasses import dataclass
import itertools
import pandas as pd
from tqdm import tqdm


N = 50
M = 25
ELITE = 50
POPULATION = 18000
MAXITER = 100
MUTATION_PROB = 0.9
FUNCTIONAL_MIN = 1e-5


def loss(fen):
    return np.sqrt(np.sum(np.power(np.array(fen), 2)))


class Individual:

    def __init__(self, gen):
        self.gen = list(gen)
        self.fen = self.calc_fen()
        self.fitness = loss(self.fen)

    def calc_fen(self):
        i = 0
        fen = []
        while i < len(self.gen):
            fen.append(self.gen[i]+self.gen[i+1])
            i += 2
        return fen


def create_child(p1, p2, mutation_prob):
    gen = krossingover(p1, p2)
    if np.random.random() < mutation_prob:
        gen = mutation(gen)
    return Individual(gen)


def krossingover(p1, p2):
    kross_idx = random.randint(1, N) # чтобы хотя бы 1 ген менялся
    return p1.gen[:kross_idx] + p2.gen[kross_idx:]


def mutation(gen):
    idx = np.random.randint(0, N-1)
    max_delta = abs(gen[idx])
    # [-max_delta; max_delta]
    mutation_value = 2 * max_delta * np.random.random_sample() - max_delta
    gen[idx] += mutation_value
    return gen


def create_population(parents, population_size, elite_size, mutation_prob):
    children = []
    for p1 in parents[:elite_size]:
        p2 = parents[np.random.randint(0, elite_size)]
        for _ in range(population_size//elite_size):
            children.append(create_child(p1, p2, mutation_prob))
    return children


def create_random_population(population_size):
    return [Individual(gen) for gen in 200 * np.random.random_sample((1000, N)) - 100]


def sort_population(population):
    return sorted(population, key=lambda x: x.fitness)


def get_best_individuals(population, elite_size):
    return sort_population(population)[:elite_size]


def sga(population_size, elite_size, mutation_prob):
    np.random.seed(12)
    population = create_random_population(population_size)
    best_individ = sort_population(population)[0]

    train_data = []
    for it in tqdm(range(MAXITER)):
        parents = get_best_individuals(population, elite_size)
        population = create_population(parents, population_size, elite_size, mutation_prob)

        generation_best = sort_population(population)[0]
        if best_individ.fitness > generation_best.fitness:
            best_individ = generation_best

        train_data.append((it+1, generation_best.fitness, best_individ.fitness))

        if best_individ.fitness < FUNCTIONAL_MIN:
            break
    return pd.DataFrame(train_data, columns=['iter', 'generation_best', 'best'])


if __name__ == '__main__':
    df = sga(POPULATION, ELITE, MUTATION_PROB)
    df.to_excel('./sga.xlsx', index=False)
