# ���D�ѨM
## .NET6�ϥ�Incoude()�ɥX�{����
�ЦbProgram.cs���K�[�H�U�]�m
```c#
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => 
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

```