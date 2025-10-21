using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1
{
    public class CalculatorModel
    {
        private string _currentInput;

        private readonly List<decimal> _operands;

        private readonly List<char> _operators;

        public string CurrentDisplay => _currentInput;

        public string FullExpressionDisplay
        {
            get
            {
                var expressionParts = new List<string>();
                for (int i = 0; i < _operators.Count; i++)
                {
                    expressionParts.Add(_operands[i].ToString());
                    expressionParts.Add(_operators[i].ToString());
                }
                return string.Join(" ", expressionParts);
            }
        }

        public CalculatorModel()
        {
            _operands = new List<decimal>();
            _operators = new List<char>();
            _currentInput = "0";
        }

        public void AddDigit(char digit)
        {
            if (_currentInput == "0")
            {
                _currentInput = digit.ToString();
            }
            else
            {
                _currentInput += digit;
            }
        }

        public void AddDecimalPoint()
        {
            if (!_currentInput.Contains('.'))
            {
                _currentInput += '.';
            }
        }

        public void SetOperation(char operation)
        {
            if (string.IsNullOrEmpty(_currentInput) && _operators.Count > 0)
            {
                _operators[_operators.Count - 1] = operation;
                return;
            }

            CommitCurrentInput();

            _operators.Add(operation);
        }

        public void CalculateResult()
        {
            CommitCurrentInput();

            if (_operands.Count == 0) return;
            if (_operands.Count != _operators.Count + 1)
            {
                _currentInput = _operands[0].ToString();
                _operands.Clear();
                _operands.Add(Convert.ToDecimal(_currentInput));
                return;
            }

            var tempOperands = new List<decimal>(_operands);
            var tempOperators = new List<char>(_operators);

            for (int i = 0; i < tempOperators.Count; i++)
            {
                if (tempOperators[i] == '*' || tempOperators[i] == '/')
                {
                    decimal result = PerformOperation(tempOperands[i], tempOperands[i + 1], tempOperators[i]);

                    tempOperands[i] = result;
                    tempOperands.RemoveAt(i + 1);

                    tempOperators.RemoveAt(i);

                    i--;
                }
            }

            decimal finalResult = tempOperands[0];
            for (int i = 0; i < tempOperators.Count; i++)
            {
                finalResult = PerformOperation(finalResult, tempOperands[i + 1], tempOperators[i]);
            }

            ClearAll();
            _currentInput = finalResult.ToString();
        }

        public void ClearEntry()
        {
            _currentInput = "0";
        }

        public void ClearAll()
        {
            _operands.Clear();
            _operators.Clear();
            _currentInput = "0";
        }
        public void Backspace()
        {
            if (_currentInput.Length > 1)
            {
                _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
            }
            else
            {
                _currentInput = "0";
            }
        }

        private void CommitCurrentInput()
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                if (decimal.TryParse(_currentInput, out decimal number))
                {
                    _operands.Add(number);
                }
                _currentInput = string.Empty;
            }
        }
        private decimal PerformOperation(decimal a, decimal b, char op)
        {
            switch (op)
            {
                case '*': return a * b;
                case '/':
                    if (b == 0) { return 0; }
                    return a / b;
                case '+': return a + b;
                case '-': return a - b;
                default:
                    throw new ArgumentException("Невідомий оператор.");
            }
        }
    }
}
