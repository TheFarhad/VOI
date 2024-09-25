namespace Framework.Infra.Persistence.Command;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Core.Domain.Shared.Prop;

public class CodeConverter()
    : ValueConverter<Code, Guid>(_ => _.Value, _ => Code.New(_));


public class EStringConversion
    : ValueConverter<_String, string>
{
    public EStringConversion() : base(_ => _.Value, _ => _String.Instance(_)) { }
}

public class NameConversion : ValueConverter<Name, string>
{
    public NameConversion() : base(_ => _.Value, _ => Name.Instance(_)) { }
}

public class TitleConversion
    : ValueConverter<Title, string>
{
    public TitleConversion() : base(_ => _.Value, _ => Title.Instance(_)) { }
}

public class DescriptionConversion
    : ValueConverter<Description, string>
{
    public DescriptionConversion()
        : base(_ => _.Value, _ => Description.Instance(_)) { }
}

public class FileConversion
    : ValueConverter<File, byte[]>
{
    public FileConversion() : base(_ => _.Value, _ => File.Instance(_)) { }
}

public class NationalCodeConversion
    : ValueConverter<NationalCode, string>
{
    public NationalCodeConversion() : base(_ => _.Value, _ => NationalCode.Instance(_)) { }
}

public class PriorityConversion
    : ValueConverter<Priority, int>
{
    public PriorityConversion()
        : base(_ => _.Value, _ => Priority.Instance(_)) { }
}

public class RegisterConversion
    : ValueConverter<Register, DateTime>
{
    public RegisterConversion() : base(_ => _.Value, _ => Register.Instance(_)) { }
}