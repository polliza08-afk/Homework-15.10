using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleCalculator;

public class CalculatorEngine
{
    private double _currentValue = 0; //Накопичене значення / лівий операнд
    private string _currentOperator = ""; //Поточна операція +, -, *, /
    private bool _isOperationPending = false; //Чи очікується введення нового числа

    //Властивість для отримання поточного результату
    public double CurrentValue => _currentValue;

    //Метод для встановлення початкового або поточного числа
    public void SetValue(double value)
    {
        _currentValue = value;
    }

    //Метод, коли користувач натискає операцію +, -, *, /
    public double CalculateOperation(double newValue, string newOperator)
    {
        if (_isOperationPending && !string.IsNullOrEmpty(_currentOperator))
        {
            //Якщо операція вже була, виконуємо її "на льоту"
            _currentValue = PerformCalculation(_currentValue, newValue, _currentOperator);
        }
        else
        {
            _currentValue = newValue;
        }

        _currentOperator = newOperator;
        _isOperationPending = true;
        return _currentValue;
    }

    //Метод для натискання кнопки "="
    public double CalculateEquals(double newValue)
    {
        if (!string.IsNullOrEmpty(_currentOperator))
        {
            _currentValue = PerformCalculation(_currentValue, newValue, _currentOperator);
            _currentOperator = "";
            _isOperationPending = false;
        }
        return _currentValue;
    }

    //Допоміжний приватний метод для базової математики
    private double PerformCalculation(double left, double right, string op)
    {
        return op switch
        {
            "+" => left + right,
            "-" => left - right,
            "×" or "*" => left * right,
            "÷" or "/" => right != 0 ? left / right : double.NaN, //Захист від ділення на нуль
            _ => right
        };
    }

    //Повне скидання калькулятора
    public void Clear()
    {
        _currentValue = 0;
        _currentOperator = "";
        _isOperationPending = false;
    }
}
