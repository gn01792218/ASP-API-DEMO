# ���D�ѨM
## .NET6�ϥ�Incloude()�ɥX�{����
�ЦbProgram.cs���K�[�H�U�]�m
```c#
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => 
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

```

# �u�ƤΫݿ�ƶ�
## 1.��U��Repository��CRUD�R�W�Τ@
## 2.�ϥ����Ҵ���ɡA�p��N���p��]�@�_����
## 3.��User Table�[�i��Ʈw��