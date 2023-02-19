using edk.Tools;

namespace edk.Kchef.Domain.Services
{
    internal class PasswordRule
    {
        private readonly OptionsPassword _options;
        public PasswordRule(OptionsPassword options)
        {
            _options = options;
        }
        public bool MaxLength(string passwordPlainText)
            => (_options.MaxSize < _options.MinSize || passwordPlainText.Length.IsLessThanOrEqual(_options.MaxSize));

        public bool MinLength(string passwordPlainText)
         => (_options.MinSize <= 0 || passwordPlainText.Length.IsGreaterThanOrEqual(_options.MinSize));

        public bool HasUpperCharacter(string passwordPlainText)
            => (!_options.UpperCharacter || passwordPlainText.ContainsUpper());

        public bool HasLowerCharacter(string passwordPlainText)
            => (!_options.LowerCharacter || passwordPlainText.ContainsLower());

        public bool HasDigit(string passwordPlainText)
            => (!_options.Digits || passwordPlainText.ContainsDigit());
        public bool HasSpecialCharacter(string passwordPlainText)
            => (!_options.SpecialCharacter || passwordPlainText.ContainsSpecialCharacter());

    }
}
