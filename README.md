<p align="center">
  <img src="logo.png" alt="Lengko Alert WPF Logo" width="140" />
</p>

# Lengko.Alert.Wpf — Demos & Samples

Modern, high-performance alert dialogs, modal prompts, and toast notifications for **WPF (Windows Presentation Foundation)** with zero external dependencies. Pure WPF hardware-accelerated vector rendering.

[![Platform](https://img.shields.io/badge/Platform-WPF-brightgreen.svg)]()
[![Languages](https://img.shields.io/badge/Languages-C%23%20%7C%20VB.NET%20%7C%20F%23-blue.svg)]()
[![Target](https://img.shields.io/badge/.NET-8.0--windows-512BD4.svg)]()
[![License](https://img.shields.io/badge/License-Freeware-orange.svg)](LICENSE.txt)

---

## Projects in this Folder

| Language | Project File | Description |
|---|---|---|
| **C#** | `CSharp/Lengko.Alert.Wpf.Demo.CSharp.csproj` | Comprehensive C# WPF showcase covering Success, Error, Warning, Info, Confirm, Prompt, and multi-position toasts. |
| **VB.NET** | `VB/Lengko.Alert.Wpf.Demo.VB.vbproj` | Full-featured VB.NET WPF demo demonstrating modal dialogs, animations, and non-blocking alerts. |
| **F#** | `FSharp/Lengko.Alert.Wpf.Demo.FSharp.fsproj` | Functional F# WPF demo with programmatic layout, dialog flows, and toast notifications. |

Dedicated solution: **`Lengko.Alert.Wpf.sln`** (builds all 3 demos alongside the library).

---

## Quick Start / How to Run

### Via Visual Studio
Open `Lengko.Alert.Wpf.sln`, select any demo project as the Startup Project, and press `F5`.

### Via .NET CLI

```powershell
# Run C# Demo
dotnet run --project CSharp/Lengko.Alert.Wpf.Demo.CSharp.csproj

# Run VB.NET Demo
dotnet run --project VB/Lengko.Alert.Wpf.Demo.VB.vbproj

# Run F# Demo
dotnet run --project FSharp/Lengko.Alert.Wpf.Demo.FSharp.fsproj
```

---

## Code Examples

### C#
```csharp
using Lengko.Alert.Wpf;

// 1. Success Alert
LengkoAlert.Fire("Berhasil!", "Data tersimpan dengan sukses.", AlertIcon.Success);

// 2. Confirmation Dialog
bool confirmed = LengkoAlert.Confirm("Hapus Data", "Yakin ingin menghapus record ini?", "Ya, Hapus", "Batal", AlertIcon.Warning);
if (confirmed)
{
    LengkoAlert.ShowToast("Data berhasil dihapus!", AlertIcon.Success);
}

// 3. Quick Corner Toast
LengkoAlert.ShowToast("Pembaruan sistem tersedia.", AlertIcon.Info, timerMs: 3000);
```

### VB.NET
```vb
Imports Lengko.Alert.Wpf

' 1. Success Alert
LengkoAlert.Fire("Berhasil!", "Data tersimpan dengan sukses.", AlertIcon.Success)

' 2. Confirmation Dialog
Dim confirmed As Boolean = LengkoAlert.Confirm("Hapus Data", "Yakin ingin menghapus record ini?", "Ya, Hapus", "Batal", AlertIcon.Warning)
If confirmed Then
    LengkoAlert.ShowToast("Data berhasil dihapus!", AlertIcon.Success)
End If

' 3. Quick Corner Toast
LengkoAlert.ShowToast("Pembaruan sistem tersedia.", AlertIcon.Info, timerMs:=3000)
```

### F#
```fsharp
open Lengko.Alert.Wpf

// 1. Success Alert
LengkoAlert.Fire("Berhasil!", "Data tersimpan dengan sukses.", AlertIcon.Success) |> ignore

// 2. Confirmation Dialog
let confirmed = LengkoAlert.Confirm("Hapus Data", "Yakin ingin menghapus record ini?", "Ya, Hapus", "Batal", AlertIcon.Warning)
if confirmed then
    LengkoAlert.ShowToast("Data berhasil dihapus!", AlertIcon.Success, 3000)

// 3. Quick Corner Toast
LengkoAlert.ShowToast("Pembaruan sistem tersedia.", AlertIcon.Info, 3000)
```

---

## License

**Proprietary Freeware License (PFL) © 2026 LengkoDev, Cas.**

- Free for personal and commercial use.
- Unmodified binaries may ship inside your application.
- No resale, standalone redistribution, modification, or reverse engineering.

Full terms: `LICENSE.txt` included in the package.
