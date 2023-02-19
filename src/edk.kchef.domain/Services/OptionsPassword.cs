namespace edk.Kchef.Domain.Services
{
    internal class OptionsPassword
    {
        public int MinSize { get; set; } = 8;
        public int MaxSize { get; set; } = 20;
        public bool SpecialCharacter { get; set; } = true;
        public bool UpperCharacter { get; set; } = true;
        public bool LowerCharacter { get; set; } = true;
        public bool Digits { get; set; } = true;
    }
}
