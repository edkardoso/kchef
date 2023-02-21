using edk.Tools;

namespace edk.Kchef.Domain.Entities.Users
{
    internal class PasswordRule
    {
        private readonly OptionsPasswordRule _options;
        public PasswordRule(OptionsPasswordRule options)
        {
            _options = options ?? new OptionsPasswordRule();
        }
        public bool MaxLength(string passwordPlainText)
            => _options.MaxSize < _options.MinSize || passwordPlainText.Length.IsLessThanOrEqual(_options.MaxSize);

        public bool MinLength(string passwordPlainText)
         => _options.MinSize <= 0 || passwordPlainText.Length.IsGreaterThanOrEqual(_options.MinSize);

        public bool HasUpperCharacter(string passwordPlainText)
            => !_options.UpperCharacter || passwordPlainText.ContainsUpper();

        public bool HasLowerCharacter(string passwordPlainText)
            => !_options.LowerCharacter || passwordPlainText.ContainsLower();

        public bool HasDigit(string passwordPlainText)
            => !_options.Digits || passwordPlainText.ContainsDigit();
        public bool HasSpecialCharacter(string passwordPlainText)
            => !_options.SpecialCharacter || passwordPlainText.ContainsSpecialCharacter();

    }
}
