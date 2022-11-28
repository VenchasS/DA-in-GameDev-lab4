# АНАЛИЗ ДАННЫХ И ИСКУССТВЕННЫЙ ИНТЕЛЛЕКТ [in GameDev]
Отчет по лабораторной работе #4 выполнил:
- Безбородов Вениамин Васильевич
- РИ210940
Отметка о выполнении заданий (заполняется студентом):

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.


Структура отчета

- Данные о работе: название работы, фио, группа, выполненные задания.
- Цель работы.
- Задание 1.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 2.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 3.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Выводы.


## Цель работы
Изучить работу перцептрона на реализации команд OR, AND, XOR, NAND.

## Задание 1
### Реализовать перцептрон, который умеет производить вычисления OR, AND, XOR, NAND.
Ход работы:
Был создан пустой 3D проект на Unity, к нему был подключен скрипт Perceptron.cs.
Затем код был модифицирован и вынесен в класс ```PerceptronClass```
```csharp
private class PerceptronClass
    {
        public TrainingSet[] ts;
        double[] weights = { 0, 0 };
        double bias = 0;
        double totalError = 0;

        double DotProductBias(double[] v1, double[] v2)
        {
            if (v1 == null || v2 == null)
                return -1;

            if (v1.Length != v2.Length)
                return -1;

            double d = 0;
            for (int x = 0; x < v1.Length; x++)
            {
                d += v1[x] * v2[x];
            }

            d += bias;

            return d;
        }

        double CalcOutput(int i)
        {
            double dp = DotProductBias(weights, ts[i].input);
            if (dp > 0) return (1);
            return (0);
        }

        void InitialiseWeights()
        {
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = Random.Range(-1.0f, 1.0f);
            }
            bias = Random.Range(-1.0f, 1.0f);
        }

        void UpdateWeights(int j)
        {
            double error = ts[j].output - CalcOutput(j);
            totalError += Mathf.Abs((float)error);
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = weights[i] + error * ts[j].input[i];
            }
            bias += error;
        }

        public double CalcOutput(double i1, double i2)
        {
            double[] inp = new double[] { i1, i2 };
            double dp = DotProductBias(weights, inp);
            if (dp > 0) return (1);
            return (0);
        }

        public void Train(int epochs, TrainingSet[] ts)
        {
            this.ts = ts;
            InitialiseWeights();

            for (int e = 0; e < epochs; e++)
            {
                totalError = 0;
                for (int t = 0; t < ts.Length; t++)
                {
                    UpdateWeights(t);
                    Debug.Log("W1: " + (weights[0]) + " W2: " + (weights[1]) + " B: " + bias);
                }
                Debug.Log("TOTAL ERROR: " + totalError);
            }
        }
    }
```


Затем были заполненны Training set'ы, каждый для своей операции
```OR, AND, XOR, NAND```
![Screenshot_2](https://user-images.githubusercontent.com/49115035/204156231-3e60fa89-cdef-4d87-8890-5de3b42eb95c.png)
![Screenshot_3](https://user-images.githubusercontent.com/49115035/204156225-b003d51a-e9fc-4f0d-a6d4-0bde78346e2f.png)
![Screenshot_4](https://user-images.githubusercontent.com/49115035/204156228-c912b3a8-4b14-4e90-9b4f-3202fcd53db9.png)
![Screenshot_5](https://user-images.githubusercontent.com/49115035/204156230-c547b185-0f3b-4933-8250-7604bb043581.png)

Мы инициализируем 4 обьекта класса ```PerceptronClass``` и обучаем их, каждого своей операции.

А затем выводим резальтаты обучения
```csharp
        int iterations = 8;
        var orPerceptron = new PerceptronClass();
        orPerceptron.Train(iterations, OrTs);
        var andPerceptron = new PerceptronClass();
        andPerceptron.Train(iterations, AndTs);
        var xorPerceptron = new PerceptronClass();
        xorPerceptron.Train(iterations, XorTs);
        var nandPerceptron = new PerceptronClass();
        nandPerceptron.Train(iterations, NandTs);



        Debug.Log("Test 0 or 0: " + orPerceptron.CalcOutput(0, 0));
        Debug.Log("Test 0 or 1: " + orPerceptron.CalcOutput(0, 1));
        Debug.Log("Test 1 or 0: " + orPerceptron.CalcOutput(1, 0));
        Debug.Log("Test 1 or 1: " + orPerceptron.CalcOutput(1, 1));

        Debug.Log("Test 0 and 0: " + andPerceptron.CalcOutput(0, 0));
        Debug.Log("Test 0 and 1: " + andPerceptron.CalcOutput(0, 1));
        Debug.Log("Test 1 and 0: " + andPerceptron.CalcOutput(1, 0));
        Debug.Log("Test 1 and 1: " + andPerceptron.CalcOutput(1, 1));

        Debug.Log("Test 0 xor 0: " + xorPerceptron.CalcOutput(0, 0));
        Debug.Log("Test 0 xor 1: " + xorPerceptron.CalcOutput(0, 1));
        Debug.Log("Test 1 xor 0: " + xorPerceptron.CalcOutput(1, 0));
        Debug.Log("Test 1 xor 1: " + xorPerceptron.CalcOutput(1, 1));

        Debug.Log("Test 0 nand 0: " + nandPerceptron.CalcOutput(0, 0));
        Debug.Log("Test 0 nand 1: " + nandPerceptron.CalcOutput(0, 1));
        Debug.Log("Test 1 nand 0: " + nandPerceptron.CalcOutput(1, 0));
        Debug.Log("Test 1 nand 1: " + nandPerceptron.CalcOutput(1, 1));
```
В консоли мы видим результаты. 
![Screenshot_1](https://user-images.githubusercontent.com/49115035/204156429-a45aabb4-8626-4d7a-860d-08a3ccbae3e0.png)
Однако как мы можем заметить, только у операции ```XOR``` не верные результаты, из-за того что линейная модель перцептрона не может правильно разделить одной линией подобного рода плоскость, а примерно так и выглядит операция ```XOR```.
![9af085a4c6ff4f83bcb92b6ea974fa01](https://user-images.githubusercontent.com/49115035/204156563-172dd9d6-c94b-4476-bc8e-29ad88230723.png)


Остальные же операции способны быть явно выполненны и перцептрон их выполняет без ошибок после обучения.




## Задание 2
### Построить графики зависимости количества ошибок от эпох обучения перцептрона.
Далее я написал метод получения среднего значения ошибок по эпохе
```csharp
        public double GetStatistics(int epochs, TrainingSet[] ts, int range)
        {
            var statArray = new double[range];
            for (int i = 0; i < range; i++)
            {
                Train(epochs, ts);
                statArray[i] = totalError;
            }
            var res = 0.0;
            foreach (var item in statArray)
            {
                res += item;
            }
            return res / range;
        }
```
Запускал программу для ```range = 100``` и ```epochs от 1 до 5```

получил данные результаты:
![chart (1)](https://user-images.githubusercontent.com/49115035/204158337-a020f4e7-5bc5-40a2-b56d-f74b6ff410ae.png)
![chart](https://user-images.githubusercontent.com/49115035/204158336-0bb7b8c4-7e24-48e3-a6d5-a1bd0dd76ca3.png)
![chart (2)](https://user-images.githubusercontent.com/49115035/204158335-e1cb4c7d-5069-45b9-be69-56afa9974e59.png)
![chart (3)](https://user-images.githubusercontent.com/49115035/204158333-711e7caf-e01c-43d6-b100-f1755f4a75e8.png)





## Задание 3
### Построить визуальную модель работы перцептрона


Для визуалици я выбрал взаимодействие кубов, означающих 1 - зеленый куб и 0 - красный куб,
при соприкосновении кубы выполнют ```OR``` операцию запрашивая значение из перцепртона, и перекрашиваются в результат работы перцептрона.


Новый файл [Perceptron2.cs](https://github.com/VenchasS/DA-in-GameDev-lab4/blob/main/Perceptron2.cs) который используется для визализации
![2022-11-28 15-57-16](https://user-images.githubusercontent.com/49115035/204262244-620a7c67-e02f-46b8-92e0-42decb69174c.gif)



## Выводы
В ходе лабораторной работы я научился использовать перцептрон и полноценно научил его 3 базовым операциям а так же XOR операции. Я понял как работает линейная модель перцептрона и узнал про XOR problem.



