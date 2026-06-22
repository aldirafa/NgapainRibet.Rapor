# NgapainRibet.Rapor

Aplikasi desktop offline berbasis AI lokal untuk membantu guru menyusun
Deskripsi Capaian Kompetensi pada rapor Kurikulum Merdeka — tanpa biaya
cloud API atau ketergantungan internet (selain saat download model
sekali di awal). Lihat `AGENTS.md` untuk spesifikasi lengkap proyek.

## Status

**Core engine (F#) sudah fungsional dan teruji** — domain model, prompt
builder, model download/cache manager (dengan verifikasi SHA256), dan
wrapper LLamaSharp untuk inference, semuanya sudah berhasil di-build &
di-test di Mac. **UI (VB.NET) masih skeleton** — Form kosong yang
terbukti bisa memanggil Core, belum ada layar input/konfigurasi
sesungguhnya.

Lihat `TODO.md` untuk status detail per item & catatan verifikasi.

### Yang sudah ada

- **`NgapainRibet.Rapor.Core`** (F#):
  - `DomainModels.fs` — `Student` (termasuk `Notes` permanen per-siswa),
    `DownloadState`, `AiState`.
  - `PromptBuilder.fs` — bangun system/user prompt dinamis dari checklist
    Strengths/Weaknesses/Tone + catatan guru, pakai istilah Kurikulum
    Merdeka. Murni logic, di-cover unit test.
  - `ModelManager.fs` — cek cache lokal & download GGUF dari HuggingFace
    dengan progress reporting + verifikasi integritas SHA256.
  - `AiEngine.fs` — wrapper LLamaSharp: load model (CPU-only) & jalankan
    inference dengan streaming token ke UI. Hasil dibungkus `Result` agar
    error tidak pernah tercampur dengan narasi asli.
  - `Library.fs` (`CoreInfo`) — fungsi kecil pembukti interop F#↔VB.NET.
- **`NgapainRibet.Rapor.Core.Tests`** (xUnit) — test untuk domain model &
  prompt builder (cepat, tidak butuh model/network). Ada juga satu test
  integrasi manual (`ModelManagerTests.fs`) yang di-`Skip` secara default
  karena men-download model sungguhan — baca catatan di file itu kalau
  perlu menjalankannya.
- **`NgapainRibet.Rapor.UI`** (VB.NET WinForms) — `MainWindow` kosong yang
  menampilkan status dari Core di sebuah Label. Sudah diverifikasi
  build & run di Windows.

### Belum dikerjakan

- UI sesungguhnya: input siswa, checklist Strengths/Weaknesses, pilihan
  Tone, catatan guru, progress download/inference, textbox hasil.
- Excel import & Excel/Word export — sengaja ditunda, direncanakan
  sebagai **fitur premium** (lihat `TODO.md` bagian 3).

## Prasyarat untuk build

1. **.NET 10.0 SDK** terpasang (`dotnet --version`).
2. Akses internet ke **nuget.org** untuk restore (LLamaSharp + backend
   CPU-nya cukup besar, puluhan MB).
3. UI project (`net10.0-windows` + WinForms) hanya bisa di-build/jalan
   di **Windows**. Core sendiri cross-platform (F#, sudah diverifikasi
   build & test di Mac).

## Cara membuka

```
dotnet restore NgapainRibet.Rapor.slnx
dotnet build NgapainRibet.Rapor.slnx
dotnet test tests/NgapainRibet.Rapor.Core.Tests
```

Atau buka `NgapainRibet.Rapor.slnx` langsung di Visual Studio 2026+.

## Struktur folder

```
NgapainRibet.Rapor/
├── NgapainRibet.Rapor.slnx
├── AGENTS.md                            ← spesifikasi proyek lengkap
├── TODO.md                              ← status & langkah selanjutnya
├── src/
│   ├── NgapainRibet.Rapor.Core/         (F#)
│   │   ├── NgapainRibet.Rapor.Core.fsproj
│   │   ├── DomainModels.fs
│   │   ├── PromptBuilder.fs
│   │   ├── ModelManager.fs
│   │   ├── AiEngine.fs
│   │   └── Library.fs
│   └── NgapainRibet.Rapor.UI/           (VB.NET WinForms — skeleton)
│       ├── NgapainRibet.Rapor.UI.vbproj
│       ├── Program.vb
│       ├── MainWindow.vb
│       ├── MainWindow.Designer.vb
│       └── My Project/
└── tests/
    └── NgapainRibet.Rapor.Core.Tests/   (xUnit)
```

## Langkah selanjutnya

Lihat `TODO.md` bagian 2 untuk daftar layar UI yang direncanakan
(checklist Strengths/Weaknesses, pilihan Tone custom, catatan guru,
rich text box hasil, dst).
