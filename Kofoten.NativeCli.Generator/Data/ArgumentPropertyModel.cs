using Microsoft.CodeAnalysis;

namespace Kofoten.NativeCli.Generator.Data;

internal record ArgumentPropertyModel(
    string Name,
    string TypeName,
    string ValueTypeName,
    string? KeyTypeName,
    SpecialType SpecialType,
    SpecialType KeySpecialType,
    SpecialType ValueSpecialType,
    bool IsCollection,
    bool IsDictionary,
    CollectionType CollectionType,
    bool IsEnum,
    bool IsFlagsEnum,
    string Description,
    string ValueParseMethodName,
    bool ValueHasErrorMessageOut,
    string? KeyParseMethodName,
    bool KeyHasErrorMessageOut,
    int Position
) : PropertyModel(
    Name,
    TypeName,
    ValueTypeName,
    KeyTypeName,
    SpecialType,
    KeySpecialType,
    ValueSpecialType,
    true,
    IsCollection,
    IsDictionary,
    CollectionType,
    IsEnum,
    IsFlagsEnum,
    Description,
    ValueParseMethodName,
    ValueHasErrorMessageOut,
    KeyParseMethodName,
    KeyHasErrorMessageOut);