# remove-utf8-bom

CLI tool to remove UTF-8 BOM (byte order mark) from source files recursively in a directory. Processes `.cs`, `.cshtml`, `.js`, `.css`, `.html`, `.json`, `.xml`, `.txt`, `.sh`, `.md`, `.csproj`, and `.sln` files.

## Usage

```
remove-utf8-bom <path to directory>
```

### Example

```
remove-utf8-bom ./src
Removed BOM from ./src/Program.cs
Modified 1 files out of 42.
```

## License

This project is licensed under the [MIT License](LICENSE).
