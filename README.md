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



## Задание 3
### Доработайте сцену и обучите ML-Agent таким образом, чтобы шар
перемещался между двумя кубами разного цвета. Кубы должны, как и в первом
задании, случайно изменять координаты на плоскости.

Был изменен прошлый [скрипт](https://github.com/VenchasS/DA-in-GameDev-lab3/blob/main/RollerAgent.cs) и код стал следующим
```css
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent : Agent
{
    Rigidbody rBody;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public Transform Target;
    public Transform Target2;

    public override void OnEpisodeBegin()
    {
        if (this.transform.localPosition.y < 0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
        }

        Target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
        Target2.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(Target2.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }
    public float forceMultiplier = 10;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);

        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);
        float distanceToTarget2 = Vector3.Distance(this.transform.localPosition, Target2.localPosition);
        float distanceBetween = Vector3.Distance(Target.localPosition, Target2.localPosition) / 2;


        if(distanceToTarget > distanceToTarget2 - 0.4 && distanceToTarget < distanceToTarget2 + 0.4 && distanceToTarget2 < distanceBetween + 0.4)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        else if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }
}
```

после ```110 000 steps ``` шар хорошо научился двигаться в центр между кубами, и в случае когда у него не получалось встать между кубами из-за того что кубы появились слишком близко он пытается слететь с арены


Ниже приведен пример работы, gif
![4](https://user-images.githubusercontent.com/49115035/198097247-84f5d66c-41d8-4320-9805-43043c2a9273.gif)


## Выводы
В ходе лабораторной работы я научился подключать ml agenta, создавать системы машинного обучения и интегрировать ее с Unity.



