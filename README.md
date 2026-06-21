# NgapainRibet.Rapor — Solution Scaffold

Skeleton awal untuk **NgapainRibet.Rapor**, sesuai spesifikasi proyek
(F# Core + VB.NET WinForms UI, di atas .NET 10.0).

## Status

Ini adalah **scaffolding saja** — kerangka solusi yang bisa di-build,
belum berisi logic AI, domain model lengkap, atau UI sesungguhnya.
Tujuannya: memastikan struktur project & reference antar-bahasa
(F# ↔ VB.NET) benar sebelum kita membangun logic di atasnya.

Yang sudah ada:
- `NgapainRibet.Rapor.Core` (F#) — berisi `CoreInfo.getStatusMessage()`,
  fungsi sederhana untuk membuktikan VB.NET bisa memanggil F#.
- `NgapainRibet.Rapor.UI` (VB.NET WinForms) — Form kosong yang
  menampilkan status dari Core di sebuah Label.
- `DomainModels.fs` — placeholder kosong. Desain `Student`,
  `DownloadState`, `AiState` (bagian 4 spesifikasi) **belum** dikerjakan;
  ini langkah terpisah berikutnya.
- `LLamaSharp` / `LLamaSharp.Backend.Cpu` — **belum** ditambahkan ke
  `.fsproj` (di-comment sebagai TODO), supaya kita tidak menyeret
  dependency besar sebelum kontrak Core jelas.

## Prasyarat untuk build di mesinmu

⚠️ **Penting**: project ini dibuat di sandbox tanpa .NET SDK terpasang
dan tanpa akses ke nuget.org, jadi belum pernah benar-benar di-*build*
atau di-*restore*. Sebelum membuka di Visual Studio / `dotnet build`,
pastikan:

1. **.NET 10.0 SDK** terpasang (cek dengan `dotnet --version`).
2. Mesin punya **akses internet ke nuget.org** untuk restore package
   nantinya (terutama setelah LLamaSharp ditambahkan).
3. UI project menggunakan `net10.0-windows` + WinForms — jadi hanya
   bisa di-build/dijalankan di **Windows** (atau via Visual Studio
   di Windows). F# Core sendiri sebenarnya cross-platform.

## Cara membuka

```
dotnet restore NgapainRibet.Rapor.sln
dotnet build NgapainRibet.Rapor.sln
```

Atau buka `NgapainRibet.Rapor.sln` langsung di Visual Studio 2026+.

## Struktur folder

```
NgapainRibet.Rapor/
├── NgapainRibet.Rapor.sln
├── src/
│   ├── NgapainRibet.Rapor.Core/        (F#)
│   │   ├── NgapainRibet.Rapor.Core.fsproj
│   │   ├── DomainModels.fs             (placeholder)
│   │   └── Library.fs                  (CoreInfo — fungsi tes interop)
│   └── NgapainRibet.Rapor.UI/          (VB.NET WinForms)
│       ├── NgapainRibet.Rapor.UI.vbproj
│       ├── Program.vb
│       ├── Form1.vb
│       └── Form1.Designer.vb
└── .gitignore
```

## Langkah selanjutnya yang disarankan

1. Desain domain model F# yang sesungguhnya (`Student`, `DownloadState`,
   `AiState`) di `DomainModels.fs`.
2. Tambahkan `LLamaSharp` & `LLamaSharp.Backend.Cpu` ke `.fsproj` setelah
   kontrak Core (interface yang dipanggil UI) jelas.
3. Bangun UI sesungguhnya: input data siswa, checklist kekuatan/kelemahan,
   pilihan tone.
