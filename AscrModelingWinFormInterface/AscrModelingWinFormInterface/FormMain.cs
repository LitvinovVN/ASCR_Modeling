using AscrModelingMatrixesClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AscrModelingWinFormInterface
{
    public partial class FormMain : Form
    {
        static Stopwatch stopWatch = new Stopwatch();

        public FormMain()
        {
            InitializeComponent();
        }                

        private void FormMain_Load(object sender, EventArgs e)
        {
            stopWatch.Restart();
            MatrixCSR mCsr = new MatrixCSR(10, 3);
            stopWatch.Stop();
        }
    }
}


/////////////////////////////////////////////
//static Random random = new Random();
//static int scale = 10;
//static int scaleRightSide = 1000;

//static int n = 10;
//static SparseMatrix matrix = new SparseMatrix(n);
//static double[] rightSide = new double[n];
////Выделяем память под результат расчета
//static double[] x = new double[matrix.RowCount];

//static string matrixFormatString = "N3";
//static string rightSideFormatString = "N5";

//static void Main(string[] args)
//{
//    CudaSolveSparse css = new CudaSolveSparse();
//    CudaRandDevice cudaRandDevice = new CudaRandDevice(GeneratorType.PseudoDefault);
//    CudaDeviceVariable<float> cdv = new CudaDeviceVariable<float>(new SizeT(10));
//    float mean = 5;
//    float stddev = 1;
//    cudaRandDevice.GenerateNormal(cdv, mean, stddev);
//    //stopWatch.Start();
//    //GenerateAndSaveRandomDataToFile("data.txt", 100000000);
//    //stopWatch.Stop();
//    //Console.WriteLine("OK");
//    //Console.WriteLine($"\n Время генерирования и записи в файл случайных чисел, сек: {(double)stopWatch.ElapsedMilliseconds / 1000}");
//    //stopWatch.Reset();

//    //stopWatch.Start();
//    //GenerateAndSaveRandomDataToBinaryFile("data.dat", 100000000);
//    //stopWatch.Stop();            
//    //Console.WriteLine($"\n Время генерирования и записи случайных чисел в бинарный файл, сек: {(double)stopWatch.ElapsedMilliseconds / 1000}");
//    //stopWatch.Reset();

//    Console.Write("Инициализация матрицы СЛАУ... ");
//    //FillSparseMatrixRnd(matrix, scale);
//    //FillSparseMatrixDiagNull(matrix, 3);
//    stopWatch.Start();
//    //FillSparseMatrixDiagonalsRnd(matrix, scale, 3);
//    //FillSparseMatrixDiagonalsRndFromFile("data.txt", matrix, 100, 3);
//    FillSparseMatrixDiagonalsRndFromBinaryFile("data.dat", matrix, 100, 3);
//    FillArrayRnd(rightSide, scaleRightSide);

//    stopWatch.Stop();
//    Console.WriteLine("OK");
//    Console.WriteLine($"\n Время выполнения инициализации массивов, сек: {(double)stopWatch.ElapsedMilliseconds / 1000}");
//    stopWatch.Reset();
//    SolveSlaeCuda();

//    //Report(false, false);
//    Report();

//    MatrixScrTest(10000, 7);

//}

//private static void MatrixScrTest(int N, int NumDiagonals)
//{
//    Console.Write("------------------------");
//    Console.Write("Инициализация матрицы MatrixCSR... ");
//    stopWatch.Reset();
//    stopWatch.Start();
//    //MatrixCSR matrixCSR = new MatrixCSR(N, NumDiagonals);
//    //matrixCSR.GenerateRandomData(100);
//    //matrixCSR.GenerateCsrColIndA_and_CsrRowPtrA();
//    stopWatch.Stop();
//    Console.WriteLine($"\nВремя выполнения инициализации матрицы MatrixCSR, сек: {(double)stopWatch.ElapsedMilliseconds / 1000}");

//    //matrixCSR.ExportToXml("matrixCSR.ExportToXml.xml");
//    MatrixCSR matrixCSR = MatrixCSR.ImportFromXml("matrixCSR.ExportToXml.xml");

//    double[] rSide = new double[N];
//    FillArrayRnd(rSide, 100);

//    double[] result = new double[N];

//    CudaSolveSparse sp = new CudaSolveSparse(); //Создаем решатель из библиотеки ManagedCuda
//    CudaSparseMatrixDescriptor matrixDescriptor = new CudaSparseMatrixDescriptor(); // Создается дескриптор матрицы
//    double tolerance = 0.0000001; //Точность расчета. Значение взято для иллюстрации

//    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: Запуск решения СЛАУ...");
//    stopWatch.Restart();
//    sp.CsrlsvluHost(matrixCSR.NumRows, matrixCSR.DiagonalsElementsNumber, matrixDescriptor, matrixCSR.CsrValA,
//        matrixCSR.CsrRowPtrA, matrixCSR.CsrColIndA, rSide,
//        tolerance, 0, result); //Решение СЛАУ методом LU факторизации            
//    stopWatch.Stop();
//    Console.WriteLine($"\nВремя решения СЛАУ, сек: {(double)stopWatch.ElapsedMilliseconds / 1000}");
//    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: Запуск проверки решения СЛАУ...");
//    bool isCorrect = CheckSlaeResult(matrixCSR, rSide, result, 0.000000001);
//    Console.WriteLine(isCorrect);
//}

///// <summary>
///// Проверка решения СЛАУ
///// </summary>
///// <param name="matrixCSR"></param>
///// <param name="rSide"></param>
///// <param name="result"></param>
///// <param name="maxError">Максимальное допустимое отклонение</param>
///// <returns></returns>
//private static bool CheckSlaeResult(MatrixCSR matrixCSR, double[] rSide, double[] result, double maxError = 0.001)
//{
//    for (int i = 0; i < matrixCSR.NumRows; i++)
//    {
//        double tempRowResult = 0;
//        for (int j = 0; j < matrixCSR.NumCols; j++)
//        {
//            tempRowResult += matrixCSR[i, j] * result[j];
//        }

//        if ((rSide[i] - tempRowResult) > maxError)
//            return false;
//    }
//    return true;
//}

//private static void FillSparseMatrixDiagonalsRndFromBinaryFile(string fileName,
//    SparseMatrix mtx,
//    int scale,
//    int numDiagonalsNotNull)
//{
//    try
//    {
//        // создаем объект BinaryReader
//        using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
//        {
//            mtx.Clear();

//            try
//            {
//                int counter = 0;
//                Console.WriteLine();
//                Console.WriteLine("******считываем бинарный файл********");
//                for (int i = 0; i < mtx.RowCount; i++)
//                {
//                    for (int j = i - numDiagonalsNotNull + 1; j < i + numDiagonalsNotNull; j++)
//                    {
//                        if (i < 0 || j < 0) continue;
//                        if (i >= mtx.RowCount || j >= mtx.RowCount) continue;

//                        //if (reader.PeekChar() > -1)
//                        //{
//                        //Console.WriteLine(line);
//                        mtx[i, j] = reader.ReadDouble() * scale;

//                        counter++;
//                        if (counter == 1000)
//                            Console.WriteLine($"1000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 10000)
//                            Console.WriteLine($"10000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 25000)
//                            Console.WriteLine($"25000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 50000)
//                            Console.WriteLine($"50000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 75000)
//                            Console.WriteLine($"75000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 100000)
//                            Console.WriteLine($"100000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 200000)
//                            Console.WriteLine($"200000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 300000)
//                            Console.WriteLine($"300000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 400000)
//                            Console.WriteLine($"400000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 500000)
//                            Console.WriteLine($"500000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 1000000)
//                            Console.WriteLine($"1000000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 1500000)
//                            Console.WriteLine($"1500000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 2000000)
//                            Console.WriteLine($"2000000: [{i}, {j}] = {mtx[i, j]}");
//                        if (counter == 2500000)
//                            Console.WriteLine($"2500000: [{i}, {j}] = {mtx[i, j]}");
//                        //}
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.Message);
//            }
//        }
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }
//}


///// <summary>
///// Заполняет диагонали матрицы данными из файла
///// </summary>
///// <param name="fileName"></param>
///// <param name="mtx">Матрица</param>
///// <param name="scale">Коэффициент, на который будет умножено каждое число</param>
///// <param name="numDiagonalsNotNull">Число ненулевых диагоналей матрицы</param>
//private static void FillSparseMatrixDiagonalsRndFromFile(string fileName,
//    SparseMatrix mtx,
//    int scale,
//    int numDiagonalsNotNull)
//{
//    mtx.Clear();

//    try
//    {
//        Console.WriteLine();
//        Console.WriteLine("******считываем построчно********");
//        using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default))
//        {
//            string line;

//            for (int i = 0; i < mtx.RowCount; i++)
//            {
//                for (int j = i - numDiagonalsNotNull + 1; j < i + numDiagonalsNotNull; j++)
//                {
//                    if (i < 0 || j < 0) continue;
//                    if (i >= mtx.RowCount || j >= mtx.RowCount) continue;

//                    if ((line = sr.ReadLine()) != null)
//                    {
//                        //Console.WriteLine(line);
//                        mtx[i, j] = Convert.ToDouble(line) * scale;
//                    }
//                }
//            }
//        }
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }
//}


///// <summary>
///// Генерирует случайные данные и записывает их в текстовый файл
///// </summary>
//private static void GenerateAndSaveRandomDataToTextFile(string fileName, int numElements)
//{
//    try
//    {
//        using (StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.Default))
//        {
//            Random random = new Random();
//            for (int i = 0; i < numElements; i++)
//            {
//                sw.WriteLine(random.NextDouble());
//            }
//        }
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }
//}

///// <summary>
///// Генерирует случайные данные и записывает их в бинарный файл
///// </summary>
//private static void GenerateAndSaveRandomDataToBinaryFile(string fileName, int numElements)
//{
//    try
//    {
//        using (BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate)))
//        {
//            Random random = new Random();
//            for (int i = 0; i < numElements; i++)
//            {
//                // Генерируем знак
//                if (random.NextDouble() > 0.5)
//                    bw.Write(random.NextDouble());
//                else
//                    bw.Write(-random.NextDouble());
//            }
//        }
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }
//}

//private static void SolveSlaeCuda()
//{
//    // Информация о разреженной матрице в CSR формате
//    var storage = matrix.Storage as SparseCompressedRowMatrixStorage<double>;

//    //Получаем ненулевые элементы матрицы
//    var nonZeroValues = storage.EnumerateNonZero().ToArray();

//    CudaSolveSparse sp = new CudaSolveSparse(); //Создаем решатель из библиотеки ManagedCuda
//    CudaSparseMatrixDescriptor matrixDescriptor = new CudaSparseMatrixDescriptor(); // Создается дескриптор матрицы
//    double tolerance = 0.0000001; //Точность расчета. Значение взято для иллюстрации

//    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: Запуск решения СЛАУ...");
//    stopWatch.Start();
//    sp.CsrlsvluHost(matrix.RowCount, nonZeroValues.Length, matrixDescriptor, nonZeroValues,
//        storage.RowPointers, storage.ColumnIndices, rightSide,
//        tolerance, 0, x); //Решение СЛАУ методом LU факторизации
//                          //sp.CsrlsvcholHost(matrix.RowCount, nonZeroValues.Length,matrixDescriptor,)
//    stopWatch.Stop();
//}

///// <summary>
///// Заполняет переданный одномерный массив случайными числами
///// </summary>
///// <param name="array"></param>
///// <param name="scale"></param>
//private static void FillArrayRnd(double[] array, int scale)
//{
//    Random random = new Random();
//    for (int k = 0; k < array.Length; k++)
//    {
//        array[k] = random.NextDouble() * scale;
//    }
//}

///// <summary>
///// Заполняет переданную матрицу случайными числами
///// </summary>
//private static void FillSparseMatrixRnd(SparseMatrix mtx, int scale)
//{
//    Random random = new Random();
//    mtx.Clear();

//    for (int i = 0; i < mtx.RowCount; i++)
//    {
//        for (int j = 0; j < mtx.RowCount; j++)
//        {
//            mtx[i, j] = random.NextDouble() * scale;
//        }
//    }
//}

///// <summary>
///// Заполняет переданную матрицу случайными числами
///// в numDiagonalsNotNull диагоналях начиная с главной с обеих сторон
///// </summary>
//private static void FillSparseMatrixDiagonalsRnd(SparseMatrix mtx, int scale, int numDiagonalsNotNull)
//{
//    Random random = new Random();
//    mtx.Clear();

//    //Parallel.For(0, mtx.RowCount, (i, state) => {
//    //    int start = i - numDiagonalsNotNull + 1;
//    //    if (start < 0) start = 0;
//    //    for (int j = start; j < i + numDiagonalsNotNull; j++)
//    //    {
//    //        if (i < 0 || j < 0) continue;
//    //        if (i >= mtx.RowCount || j >= mtx.RowCount) continue;
//    //        mtx[i, j] = random.NextDouble() * scale;
//    //    }
//    //});

//    for (int i = 0; i < mtx.RowCount; i++)
//    {
//        for (int j = i - numDiagonalsNotNull + 1; j < i + numDiagonalsNotNull; j++)
//        {
//            if (i < 0 || j < 0) continue;
//            if (i >= mtx.RowCount || j >= mtx.RowCount) continue;
//            mtx[i, j] = random.NextDouble() * scale;
//        }
//    }
//}

///// <summary>
///// Обнуляет диагонали переданной матрицы кроме numDiagonalsNotNull,
///// начиная с главной с обеих сторон
///// </summary>
///// <param name="mtx"></param>
///// <param name="numDiagonalsNotNull">Число ненулевых диагоналей</param>
//private static void FillSparseMatrixDiagNull(SparseMatrix mtx, int numDiagonalsNotNull)
//{
//    for (int i = 0; i < mtx.RowCount; i++)
//    {
//        for (int j = 0; j < mtx.RowCount; j++)
//        {
//            if ((i - numDiagonalsNotNull) >= j)
//                mtx[i, j] = 0;

//            if ((i + numDiagonalsNotNull) <= j)
//                mtx[i, j] = 0;
//        }
//    }
//}

///// <summary>
///// Отчет
///// </summary>
//private static void Report(bool isPrintMatrixElements = true,
//    bool isPrintMatrixResults = true)
//{
//    if (isPrintMatrixResults)
//    {
//        for (int i = 0; i < n; i++)
//        {
//            double tempRowResult = 0;
//            for (int j = 0; j < n; j++)
//            {
//                if (isPrintMatrixElements)
//                {
//                    Console.Write($"{matrix[i, j].ToString(matrixFormatString)}\t");
//                }
//                tempRowResult += matrix[i, j] * x[j];
//            }
//            Console.Write($"| {rightSide[i].ToString(rightSideFormatString)}\t");
//            Console.WriteLine($"|| {tempRowResult.ToString(rightSideFormatString)}");
//        }
//    }

//    Console.WriteLine($"\n Время выполнения расчета, сек: {(double)stopWatch.ElapsedMilliseconds / 1000}");
//}