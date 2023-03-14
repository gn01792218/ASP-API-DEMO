# 問題解決
## .NET6使用Incoude()時出現報錯
請在Program.cs中添加以下設置
```c#
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => 
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

```