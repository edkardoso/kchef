﻿namespace edk.Kchef.Domain.Entities.Users;

internal record OptionsPasswordRule(
    int MinSize = 8
    , int MaxSize = 20
    , bool SpecialCharacter = true
    , bool UpperCharacter = true
    , bool LowerCharacter = true
    , bool Digits = true);
