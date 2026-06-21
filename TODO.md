# TODO — NgapainRibet.Rapor

Lacak progres di sini. Karena Core (F#) dikembangkan di Mac/VSCode dan
UI (VB.NET) dikembangkan terpisah di Windows, file ini jadi sumber
kebenaran tunggal soal apa yang sudah & belum dikerjakan — supaya
status kedua sisi tidak hilang saat pindah mesin.

Centang `[x]` kalau sudah selesai & sudah diverifikasi build/run di
mesin yang relevan (Mac untuk Core, Windows untuk UI).

---

## 0. Scaffolding

- [x] Struktur solusi (.sln) + kedua project (Core .fsproj, UI .vbproj)
- [x] Reference UI → Core terpasang
- [x] `dotnet restore` + `dotnet build` untuk **Core** — diverifikasi di Mac (sebelum LLamaSharp ditambahkan)
- [x] `dotnet restore` + `dotnet build` untuk **Core** dengan LLamaSharp — belum diverifikasi ulang setelah `AiEngine.fs` ditambahkan ==> all good, no errors
- [x] `dotnet test` untuk project test (`NgapainRibet.Rapor.Core.Tests`) — belum diverifikasi ==> all good, no errors
- [ ] `dotnet restore` + `dotnet build` untuk **UI** — belum diverifikasi (perlu Windows)
- [ ] Build solusi penuh (`dotnet build NgapainRibet.Rapor.sln`) dari root — belum diverifikasi

## 1. Core (F#) — dikerjakan di Mac/VSCode

- [x] Desain domain model sesungguhnya di `DomainModels.fs`
  - [x] `Student` (Name, Strengths, Weaknesses, Tone)
  - [x] `DownloadState` (NotStarted | Downloading of float | Completed | Error of string)
  - [x] `AiState` (Uninitialized | LoadingModel | Ready | Generating | Failed of string)
- [x] Hapus `ScaffoldPlaceholder` setelah domain model asli ada
- [x] Tambahkan `LLamaSharp` + `LLamaSharp.Backend.Cpu` ke `.fsproj` (versi 0.27.0, cek lagi saat akan restore — versi cepat berubah)
- [x] Logic download & cache model GGUF dari HuggingFace (`ModelManager.fs`)
  - [x] Cek apakah file `.gguf` sudah ada di cache lokal
  - [x] Download dengan progress reporting (`DownloadState`)
  - [ ] Verifikasi end-to-end (download sungguhan dari HuggingFace) — belum dicoba ==> cara cobanya gimana?
- [x] Loading model via LLamaSharp (CPU-only, context window kecil 512–1024 token)
  - [x] Kode ditulis di `AiEngine.fs` (`loadModelAsync`)
  - [x] **Belum pernah di-compile** — sandbox tidak punya .NET SDK/akses NuGet. Compile di Mac dulu, perbaiki kalau ada mismatch tipe kecil di `ModelParams` ==> Compile di mac berhasil tanpa ada type mismatch
- [x] Builder prompt sistem dinamis dari checklist (Strengths/Weaknesses/Tone) — pakai istilah Kurikulum Merdeka (Capaian Pembelajaran, Bimbingan, Kompetensi)
  - [x] Kode ditulis & **diuji** (`PromptBuilder.fs` + test xUnit) — ini murni logic, tidak butuh LLamaSharp, jadi confidence tinggi ==> test xUnit berhasil semua
- [x] Fungsi inference (generate narasi) — expose sebagai `Task<string>` / streaming yang gampang dikonsumsi VB.NET
  - [x] Kode ditulis di `AiEngine.fs` (`generateAsync`, streaming lewat `Action<string>`)
  - [x] **Belum pernah di-compile/jalan** — sama seperti `loadModelAsync` di atas ==> so far gaada compile error atau build error
- [x] Unit test untuk domain logic & prompt builder (`NgapainRibet.Rapor.Core.Tests`, xUnit)
  - [x] Jalankan `dotnet test` di Mac untuk konfirmasi benar-benar lulus ==> aman semua

## 2. UI (VB.NET WinForms) — dikerjakan di Windows

- [ ] Verifikasi scaffold build & run (lihat label status dari Core muncul di Form)
- [ ] Layar input data siswa (manual + import Excel)
- [ ] Layar konfigurasi per siswa:
  - [ ] Pilihan Subject
  - [ ] Checklist Strengths (Capaian Tertinggi)
  - [ ] Checklist Weaknesses (Capaian Terendah)
  - [ ] Pilihan Tone (Sangat Formal / Memotivasi / Tegas tapi Santun)
- [ ] Layar progress saat model belum ada (UI untuk `DownloadState`)
- [ ] Layar progress saat inference berjalan (UI untuk `AiState`)
- [ ] Text box hasil narasi + tombol "Copy to Clipboard"
- [ ] Export bulk ke Excel/Word
- [ ] Penanganan error (model gagal load, download gagal, dll.) ditampilkan ke user

## 3. Premium Features (to be implemented setelah core functionality OK semua)

- [ ] Import data siswa dari Excel (.xlsx) — **DITUNDA**, lihat catatan di bawah
- [ ] Export hasil narasi ke Excel/Word secara bulk — **DITUNDA**, lihat catatan di bawah

> **Catatan soal Excel import/export**: sengaja ditunda dari sesi ini.
> Ini perlu keputusan library terpisah (mis. ClosedXML untuk Excel,
> DocumentFormat.OpenXml untuk Word) dan testing sendiri — digabung
> sekarang berisiko bikin semuanya tergesa-gesa. Kerjakan di sesi
> berikutnya sebagai unit kerja sendiri.
>> Excel Import/export dijadikan fitur premium yang akan di-paywall nanti.

## 4. Integrasi & Polish (belakangan)

- [ ] Tes end-to-end: input siswa → generate → copy/export, di Windows dengan model sungguhan
- [ ] Cek pemakaian RAM aktual di laptop spek rendah (4–8GB)
- [ ] Packaging/distribusi (exe kecil + first-run model download)
- [ ] Dokumentasi pengguna (cara pakai untuk guru, bukan dokumentasi teknis)

---

## Cara update file ini

Setiap kali menyelesaikan sesuatu di salah satu mesin (Mac atau Windows),
update checklist yang relevan sebelum lanjut sesi berikutnya, supaya saat
pindah konteks (Mac ↔ Windows ↔ chat ini) status selalu sinkron.

Bagian yang sudah dikerjakan tapi aku ragu dikasih ==> keterangan