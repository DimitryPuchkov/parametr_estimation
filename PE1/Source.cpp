#define _USE_MATH_DEFINES

#include <iostream>
#include <cmath>
#include <vector>

using namespace std;

/*
* Структура точки
*/
struct point
{
	double x, y, z;
	point(double x, double y, double z)
	{
		this->x = x;
		this->y = y;
		this->z = z;
	}
	point()
	{
		this->x = 0.0;
		this->y = 0.0;
		this->z = 0.0;
	}
};


/*
* Структура электрода
*/
struct electrodes
{
	point A;
	point B;
	electrodes(point A, point B)
	{
		this->A = A;
		this->B = B;
	}
};

/*
* Структура линии
*/
struct line
{
	point M;
	point N;
	line(point M, point N)
	{
		this->M = M;
		this->N = N;
	}
};

/*
* Расстояние между точками
*/
double PointsDistance(point a, point b)
{
	return sqrt(pow(b.x - a.x, 2) +
				pow(b.y - a.y, 2) +
				pow(b.z - a.z, 2));
}

/*
* Вспомогательная функция вычисления значения в больших скобках формулы разности потенциалов
*/
double brackets(point A, point B, point M, point N)
{
	return ((1 / PointsDistance(B, M) - 1 / PointsDistance(A, M))
			-
			(1 / PointsDistance(B, N) - 1 / PointsDistance(A, N)));
}

/*
* Разность потенциалов
*/
double PD(electrodes Electrodes, line Line, double amperage, double sigma)
{
	double k = amperage / (2 * M_PI * sigma);
	return k * brackets(Electrodes.A, Electrodes.B, Line.M, Line.N);
}

/*
* Производная разности потенциалов по сигма
*/
double PDDerivativeBySigma(electrodes Electrodes, line Line, double amperage, double sigma)
{
	double k = (-1) * amperage / (2 * M_PI * sigma * sigma);
	return k * brackets(Electrodes.A, Electrodes.B, Line.M, Line.N);
}

/*
* Производная разности потенциалов по силе тока
*/
double PDDerivativeByAmperage(electrodes Electrodes, line Line, double sigma)
{
	double k = 1 / (2 * M_PI * sigma);
	return k * brackets(Electrodes.A, Electrodes.B, Line.M, Line.N);
}


int main()
{
	cout << scientific;
	unsigned short nLines = 3; // Кол-во линий в задаче
	unsigned short nParams = 1; // Кол-во неизвестных параметров задачи

	unsigned int i, j, k; // Итераторы

#pragma region Точки по условию

	electrodes Electrodes = electrodes(point(0, 0, 0), point(100, 0, 0));

	vector<line> Lines;
	Lines.push_back(line(point(200, 0, 0), point(300, 0, 0)));
	Lines.push_back(line(point(500, 0, 0), point(600, 0, 0)));
	Lines.push_back(line(point(1000, 0, 0), point(1100, 0, 0)));

#pragma endregion

#pragma region Истинные значения

	double properAmperage = 15; // Сила тока (I)
	double properSigma = 2;  // Удельная электрическая проводимость

#pragma endregion

#pragma region Практические данные

	vector<double> pracV;

	for (i = 0; i < nLines; i++)
		pracV.push_back(PD(Electrodes, Lines[i], properAmperage, properSigma));

#pragma endregion

#pragma region Весовые коэффициенты

	vector<double> w;
	for (i = 0; i < nLines; i++)
		w.push_back(1 / pracV[i]);

#pragma endregion

	double amperage = 0;
	double sigma = properSigma;
	double delta = 0.0;
	double eps = 1e-7;
	int iters = 0;
	
	double functional = 0.0;

	for (i = 0; i < nLines; i++)
		functional += pow(w[i]*(PD(Electrodes, Lines[i], amperage, sigma) - pracV[i]), 2);

	vector<vector<double>> A;
	vector<double> b;

	A.resize(nParams);
	b.resize(nParams);

	for (auto& vec : A)
		vec.resize(nParams);

	cout << iters << "\t" << amperage << "\t" << functional << endl;

	do
	{
		functional = 0.0;
		for (auto& vec : A)
			for (auto& x : vec)
				x = 0.0;

		for (auto& x : b)
			x = 0.0;

		for (i = 0; i < nParams; i++)
			for (j = 0; j < nParams; j++)
					for (k = 0; k < nLines; k++)
						A[i][j] += pow(w[k] * PDDerivativeByAmperage(Electrodes, Lines[k], sigma), 2);

		for (i = 0; i < nParams; i++)
			for (k = 0; k < nLines; k++)
				b[i] -= w[k] * w[k] *
				PDDerivativeByAmperage(Electrodes, Lines[k], sigma) *
				(PD(Electrodes, Lines[k], amperage, sigma) - pracV[k]);

		delta = b[0] / A[0][0];
		amperage += delta;

		for (i = 0; i < nLines; i++)
			functional += pow(w[i] * (PD(Electrodes, Lines[i], amperage, sigma) - pracV[i]), 2);

		iters++;

		cout << iters << "\t" << amperage << "\t" << functional << endl;

	} while (delta > eps && functional > eps);
	return 0;
}