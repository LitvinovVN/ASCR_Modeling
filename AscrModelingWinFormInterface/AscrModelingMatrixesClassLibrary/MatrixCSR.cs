using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AscrModelingMatrixesClassLibrary
{
    /// <summary>
    /// Двумерная квадратная матрица в csr-формате
    /// </summary>
    [Serializable]
    public class MatrixCSR
    {
        /// <summary>
        /// Массив значений
        /// </summary>
        public double[] CsrValA { get; set; }

        /// <summary>
        /// Массив 
        /// </summary>
        public int[] CsrRowPtrA { get; set; }
        public int[] CsrColIndA { get; set; }

        /// <summary>
        /// Число строк
        /// </summary>
        public int NumRows { get; set; }

        /// <summary>
        /// Число столбцов (равно числу строк)
        /// </summary>
        public int NumCols
        {
            get
            {
                return NumRows;
            }
        }

        /// <summary>
        /// Количество диагоналей матрицы
        /// </summary>
        public int NumDiagonals { get; set; }

        /// <summary>
        /// Количество элементов в диагоналях матрицы
        /// </summary>
        public int DiagonalsElementsNumber
        {
            get
            {
                int nnz = 0;
                for (int i = 1; i <= NumDiagonals; i++)
                {
                    int falseElements = i / 2;
                    nnz += NumRows - falseElements;
                }
                return nnz;
            }
        }

        /// <summary>
        /// Количество диагоналей ниже главной диагонали
        /// </summary>
        public int NumDiagonalsBottom
        {
            get
            {
                return (NumDiagonals - 1) / 2;
            }
        }

        /// <summary>
        /// Количество диагоналей выше главной диагонали
        /// </summary>
        public int NumDiagonalsTop
        {
            get
            {
                int numDiagonalsTop = NumDiagonalsBottom;
                if (NumDiagonals % 2 == 0)
                {
                    numDiagonalsTop++;
                }

                return numDiagonalsTop;
            }
        }

        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="i">Номер строки</param>
        /// <param name="j">Номер столбца</param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get
            {
                int rowFirstElementIndex = CsrRowPtrA[i];
                int rowNumElements = CsrRowPtrA[i + 1] - CsrRowPtrA[i];

                for (int rowElementsIndexer = rowFirstElementIndex; rowElementsIndexer < rowFirstElementIndex + rowNumElements; rowElementsIndexer++)
                {
                    if (CsrColIndA[rowElementsIndexer] == j)
                    {
                        return CsrValA[rowElementsIndexer];
                    }
                }

                return 0;
            }
            //set
            //{
            //    data[index] = value;
            //}
        }

        #region Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="n">Размер матрицы n*n</param>
        /// <param name="numDiagonals">Кол-во ненулевых диагоналей</param>
        public MatrixCSR(int n, int numDiagonals)
        {
            NumRows = n;
            NumDiagonals = numDiagonals;
            CsrValA = new double[DiagonalsElementsNumber];
            CsrRowPtrA = new int[n + 1];
            CsrColIndA = new int[DiagonalsElementsNumber];
        }

        /// <summary>
        /// Конструктор (для сериализации)
        /// </summary>
        public MatrixCSR()
        {

        }
        #endregion

        public void GenerateRandomData(double MaxValue)
        {
            double sm = MaxValue / 2;//Смещение
            Random rnd = new Random();
            Parallel.For(0, CsrValA.Length, i => {
                CsrValA[i] = rnd.NextDouble() * MaxValue - sm;
            });
        }

        /// <summary>
        /// Заполнение массива индекса столбцов для каждого элемента
        /// </summary>
        public void GenerateCsrColIndA_and_CsrRowPtrA()
        {
            int k = 0;
            try
            {
                for (int i = 0; i < NumRows; i++)
                {
                    CsrRowPtrA[i] = k;
                    for (int j = i - NumDiagonalsBottom; j <= i + NumDiagonalsTop; j++)
                    {
                        if (j < 0 || j >= NumCols) continue;
                        CsrColIndA[k] = j;
                        k++;

                        if ((i == NumRows - 1) && (j == i))
                        {
                            CsrRowPtrA[CsrRowPtrA.Length - 1] = k;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"k = {k}");
            }

        }

        #region Сериализация / десериализация
        /// <summary>
        /// Экспорт матрицы в файл XML
        /// </summary>
        /// <param name="fileName"></param>
        public void ExportToXml(string fileName)
        {
            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(MatrixCSR));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, this);
            }
        }

        /// <summary>
        /// Импорт матрицы из файла XML
        /// </summary>
        /// <param name="fileName"></param>
        public static MatrixCSR ImportFromXml(string fileName)
        {
            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(MatrixCSR));

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                MatrixCSR data = (MatrixCSR)formatter.Deserialize(fs);
                return data;
            }
        }
        #endregion
    }
}
