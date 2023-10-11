#define _USE_MATH_DEFINES

#include <iostream>
#include <cmath>
#include <vector>

using namespace std;

/*
* ��������� �����
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
* ��������� ���������
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
* ��������� �����
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
* ���������� ����� �������
*/
double PointsDistance(point a, point b)
{
	return sqrt(pow(b.x - a.x, 2) +
				pow(b.y - a.y, 2) +
				pow(b.z - a.z, 2));
}

/*
* ��������������� ������� ���������� �������� � ������� ������� ������� �������� �����������
*/
double brackets(point A, point B, point M, point N)
{
	return ((1 / PointsDistance(B, M) - 1 / PointsDistance(A, M))
			-
			(1 / PointsDistance(B, N) - 1 / PointsDistance(A, N)));
}

/*
* �������� �����������
*/
double PD(electrodes Electrodes, line Line, double amperage, double sigma)
{
	double k = amperage / (2 * M_PI * sigma);
	return k * brackets(Electrodes.A, Electrodes.B, Line.M, Line.N);
}

/*
* ����������� �������� ����������� �� �����
*/
double PDDerivativeBySigma(electrodes Electrodes, line Line, double amperage, double sigma)
{
	double k = (-1) * amperage / (2 * M_PI * sigma * sigma);
	return k * brackets(Electrodes.A, Electrodes.B, Line.M, Line.N);
}

/*
* ����������� �������� ����������� �� ���� ����
*/
double PDDerivativeByAmperage(electrodes Electrodes, line Line, double sigma)
{
	double k = 1 / (2 * M_PI * sigma);
	return k * brackets(Electrodes.A, Electrodes.B, Line.M, Line.N);
}


int main()
{
	cout << scientific;
	unsigned short nLines = 3; // ���-�� ����� � ������
	unsigned short nParams = 1; // ���-�� ����������� ���������� ������

	unsigned int i, j, k; // ���������

#pragma region ����� �� �������

	electrodes Electrodes = electrodes(point(0, 0, 0), point(100, 0, 0));

	vector<line> Lines;
	Lines.push_back(line(point(200, 0, 0), point(300, 0, 0)));
	Lines.push_back(line(point(500, 0, 0), point(600, 0, 0)));
	Lines.push_back(line(point(1000, 0, 0), point(1100, 0, 0)));

#pragma endregion

#pragma region �������� ��������

	double properAmperage = 15; // ���� ���� (I)
	double properSigma = 2;  // �������� ������������� ������������

#pragma endregion

#pragma region ������������ ������

	vector<double> pracV;

	for (i = 0; i < nLines; i++)
		pracV.push_back(PD(Electrodes, Lines[i], properAmperage, properSigma));

#pragma endregion

#pragma region ������� ������������

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