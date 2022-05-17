copy test.cs encrypt.cs

FOR /L %%I IN (1,1,3) DO (
  rename encrypt.cs test.cs
  vt test.cs test.cs
)