using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lab_1
{
    public class CalculatorModel
    {
        private string _expression = "";
        private string _history = "";

        public string CurrentExpression => string.IsNullOrEmpty(_expression) ? "0" : _expression;
        public string HistoryDisplay => _history;

        public void AddSymbol(string symbol)
        {
            if (_expression == "Помилка") _expression = "";

            if (string.IsNullOrEmpty(_expression))
            {
                if (symbol == "-" || char.IsDigit(symbol[0]))
                {
                    _expression += symbol;
                }
                return;
            }

            char lastChar = _expression.Last();
            bool isNewOp = IsOperator(symbol[0]);
            bool isLastOp = IsOperator(lastChar);

            if (isNewOp)
            {
                if (isLastOp)
                {
                    if (symbol == "-" && (lastChar == '*' || lastChar == '/'))
                    {
                        _expression += symbol;
                    }
                    else if (lastChar == '-' && _expression.Length > 1 && IsOperator(_expression[_expression.Length - 2]))
                    {
                        _expression = _expression.Substring(0, _expression.Length - 2) + symbol;
                    }
                    else
                    {
                        _expression = _expression.Substring(0, _expression.Length - 1) + symbol;
                    }
                }
                else if (lastChar == '.')
                {
                    _expression += "0" + symbol;
                }
                else
                {
                    _expression += symbol;
                }
            }
            else
            {
                _expression += symbol;
            }
        }

        public void AddDecimalPoint()
        {
            if (_expression == "Помилка") _expression = "";

            if (string.IsNullOrEmpty(_expression) || IsOperator(_expression.Last()))
            {
                _expression += "0.";
                return;
            }

            int i = _expression.Length - 1;
            while (i >= 0 && !IsOperator(_expression[i]))
            {
                if (_expression[i] == '.') return;
                i--;
            }

            _expression += ".";
        }

        public void Backspace()
        {
            if (_expression == "Помилка")
            {
                ClearAll();
                return;
            }

            if (_expression.Length > 0)
            {
                _expression = _expression.Substring(0, _expression.Length - 1);
            }
        }

        public void ClearEntry()
        {
            _expression = "";
        }

        public void ClearAll()
        {
            _expression = "";
            _history = "";
        }

        public void CalculateResult()
        {
            if (string.IsNullOrEmpty(_expression)) return;

            while (_expression.Length > 0 && (IsOperator(_expression.Last()) || _expression.Last() == '.'))
            {
                _expression = _expression.Substring(0, _expression.Length - 1);
            }

            if (string.IsNullOrEmpty(_expression)) return;

            try
            {
                string tempExpression = _expression;
                var (operands, operators) = ParseExpression(tempExpression);

                if (operands.Count == 0) return;

                for (int i = 0; i < operators.Count; i++)
                {
                    if (operators[i] == '*' || operators[i] == '/')
                    {
                        decimal res = PerformOperation(operands[i], operands[i + 1], operators[i]);
                        operands[i] = res;
                        operands.RemoveAt(i + 1);
                        operators.RemoveAt(i);
                        i--;
                    }
                }

                decimal finalResult = operands[0];
                for (int i = 0; i < operators.Count; i++)
                {
                    finalResult = PerformOperation(finalResult, operands[i + 1], operators[i]);
                }

                _history = tempExpression;
                _expression = finalResult.ToString(CultureInfo.InvariantCulture);
            }
            catch (DivideByZeroException)
            {
                _expression = "Помилка";
            }
            catch (Exception)
            {
                _expression = "Помилка";
            }
        }

        private (List<decimal>, List<char>) ParseExpression(string input)
        {
            var nums = new List<decimal>();
            var ops = new List<char>();
            string buffer = "";

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (IsOperator(c))
                {
                    if (c == '-' && (i == 0 || IsOperator(input[i - 1])))
                    {
                        buffer += c;
                    }
                    else
                    {
                        if (buffer != "")
                        {
                            nums.Add(decimal.Parse(buffer, CultureInfo.InvariantCulture));
                            buffer = "";
                        }
                        ops.Add(c);
                    }
                }
                else
                {
                    buffer += c;
                }
            }
            if (buffer != "")
            {
                nums.Add(decimal.Parse(buffer, CultureInfo.InvariantCulture));
            }

            return (nums, ops);
        }

        private bool IsOperator(char c) => "+-*/".Contains(c);

        private decimal PerformOperation(decimal a, decimal b, char op)
        {
            switch (op)
            {
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '/':
                    if (b == 0) throw new DivideByZeroException();
                    return a / b;
                default: return 0;
            }
        }
    }
}