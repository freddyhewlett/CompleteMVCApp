using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.Business.Models.Validations.Docs
{
    //Logica para validar CPF - motivos didaticos
    public class GameDevValidation
    {
        public const int GameDevDocumentLength = 11;

        public static bool Validate(string cpf)
        {
            var gameDevDocNumber = Utils.OnlyNumbers(cpf);

            if (!ValidLength(gameDevDocNumber)) return false;
            return !HasRepeatedNumbers(gameDevDocNumber) && HasValidDigit(gameDevDocNumber);
        }

        private static bool ValidLength(string value)
        {
            return value.Length == GameDevDocumentLength;
        }

        private static bool HasRepeatedNumbers(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigit(string value)
        {
            var number = value.Substring(0, GameDevDocumentLength - 2);
            var digitVerifier = new DigitVerifier(number)
                .WithMultipliersFromTo(2, 11)
                .Replacing("0", 10, 11);
            var firstDigit = digitVerifier.CalculateDigit();
            digitVerifier.AddDigit(firstDigit);
            var secondDigit = digitVerifier.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(GameDevDocumentLength - 2, 2);
        }
    }

    //Logica para validar CNPJ - motivos didaticos
    public class SwDevValidation
    {
        public const int SwDevDocLength = 14;

        public static bool Validate(string SwDevDocument)
        {
            var docNumber = Utils.OnlyNumbers(SwDevDocument);

            if (!ValidLength(docNumber)) return false;
            return !HasRepeatedDigits(docNumber) && HasValidDigits(docNumber);
        }

        private static bool ValidLength(string value)
        {
            return value.Length == SwDevDocLength;
        }

        private static bool HasRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, SwDevDocLength - 2);

            var digitVerifier = new DigitVerifier(number)
                .WithMultipliersFromTo(2, 9)
                .Replacing("0", 10, 11);
            var firstDigit = digitVerifier.CalculateDigit();
            digitVerifier.AddDigit(firstDigit);
            var secondDigit = digitVerifier.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(SwDevDocLength - 2, 2);
        }
    }

    //Logica de verificação de digitos de CPF e CNPJ - de documentação de orgãos responsáveis
    public class DigitVerifier
    {
        private string _number;
        private const int Module = 11;
        private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _substitutes = new Dictionary<int, string>();
        private bool _moduleComplement = true;

        public DigitVerifier(string number)
        {
            _number = number;
        }

        public DigitVerifier WithMultipliersFromTo(int firstMultiplier, int lastMultiplier)
        {
            _multipliers.Clear();
            for (var i = firstMultiplier; i <= lastMultiplier; i++)
                _multipliers.Add(i);

            return this;
        }

        public DigitVerifier Replacing(string replace, params int[] digits)
        {
            foreach (var i in digits)
            {
                _substitutes[i] = replace;
            }
            return this;
        }

        public void AddDigit(string digit)
        {
            _number = string.Concat(_number, digit);
        }

        public string CalculateDigit()
        {
            return !(_number.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var sum = 0;
            for (int i = _number.Length - 1, m = 0; i >= 0; i--)
            {
                var game = (int)char.GetNumericValue(_number[i]) * _multipliers[m];
                sum += game;

                if (++m >= _multipliers.Count) m = 0;
            }

            var mod = (sum % Module);
            var result = _moduleComplement ? Module - mod : mod;

            return _substitutes.ContainsKey(result) ? _substitutes[result] : result.ToString();
        }
    }

    public class Utils
    {
        public static string OnlyNumbers(string value)
        {
            var onlyNumber = "";
            foreach (var s in value)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }
    }
}
