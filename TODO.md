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

> **Catatan review (22 Juni)**: beberapa hal di bawah ini ditemukan saat
> review kode yang sudah di-push, lalu diperbaiki di sesi ini:
> - `ModelManager.fs`: `Completed` sebelumnya dilaporkan SEBELUM verifikasi
>   SHA256 — sudah diperbaiki supaya urutan benar (verifikasi dulu, baru
>   lapor Completed). File yang gagal verifikasi sekarang juga dihapus
>   supaya tidak "dipercaya" di percobaan berikutnya.
> - `ModelManagerTests.fs`: tadinya download ulang ~1.1GB + hapus cache
>   di SETIAP `dotnet test` — sekarang di-`Skip` default (manual saja) &
>   tidak menghapus cache setelah sukses.
> - `AiEngine.fs`: `generateAsync` tadinya mengembalikan error sebagai
>   string biasa (bisa tertukar dengan narasi asli) — sekarang
>   `Result<string, string>`, konsisten dengan `loadModelAsync`.
> - `Student.Notes` vs `buildUserPrompt`'s `additionalNotes` — sudah
>   dikonfirmasi: `Notes` = catatan permanen di profil siswa,
>   `additionalNotes` = catatan sekali pakai untuk generate tertentu.
>   Didokumentasikan di kode + ditambah test untuk keduanya.

- [x] Desain domain model sesungguhnya di `DomainModels.fs`
  - [x] `Student` (Name, Strengths, Weaknesses, Tone)
  - [x] `DownloadState` (NotStarted | Downloading of float | Completed | Error of string)
  - [x] `AiState` (Uninitialized | LoadingModel | Ready | Generating | Failed of string)
- [x] Hapus `ScaffoldPlaceholder` setelah domain model asli ada
- [x] Tambahkan `LLamaSharp` + `LLamaSharp.Backend.Cpu` ke `.fsproj` (versi 0.27.0, cek lagi saat akan restore — versi cepat berubah)
- [x] Logic download & cache model GGUF dari HuggingFace (`ModelManager.fs`)
  - [x] Cek apakah file `.gguf` sudah ada di cache lokal
  - [x] Download dengan progress reporting (`DownloadState`)
  - [x] Verifikasi end-to-end (download sungguhan dari HuggingFace) — belum dicoba ==> berhasil, coba manual di unit tests dengan tambahin `ModelManagerTests.fs`
- [x] Loading model via LLamaSharp (CPU-only, context window kecil 512–1024 token)
  - [x] Kode ditulis di `AiEngine.fs` (`loadModelAsync`)
  - [x] Compile di mac berhasil tanpa ada type mismatch
- [x] Builder prompt sistem dinamis dari checklist (Strengths/Weaknesses/Tone) — pakai istilah Kurikulum Merdeka (Capaian Pembelajaran, Bimbingan, Kompetensi)
  - [x] Kode ditulis & **diuji** (`PromptBuilder.fs` + test xUnit) — ini murni logic, tidak butuh LLamaSharp, jadi confidence tinggi ==> test xUnit berhasil semua
- [x] Fungsi inference (generate narasi) — expose sebagai `Task<string>` / streaming yang gampang dikonsumsi VB.NET
  - [x] Kode ditulis di `AiEngine.fs` (`generateAsync`, streaming lewat `Action<string>`)
  - [x] So far gaada compile error atau build error
- [x] Unit test untuk domain logic & prompt builder (`NgapainRibet.Rapor.Core.Tests`, xUnit)
  - [x] Jalankan `dotnet test` di Mac untuk konfirmasi benar-benar lulus ==> aman semua

## 2. UI (VB.NET WinForms) — dikerjakan di Windows

- [x] Verifikasi scaffold build & run (lihat label status dari Core muncul di Form) ==> 22 juni
- [ ] Layar input data siswa (manual)
- [ ] Layar konfigurasi per siswa:
  - [ ] Pilihan Subject
  - [ ] Checklist Strengths (Capaian Tertinggi)
      - [ ] Checklist + textbox, kita belum punya database strength and weakness. Database masukin fitur premium.
  - [ ] Checklist Weaknesses (Capaian Terendah)
      - [ ] Sama dengan strength.
  - [ ] Pilihan Tone (Sangat Formal / Memotivasi / Tegas tapi Santun)
      - [ ] Tone bisa diset custom selain disediain pilihannya.
  - [ ] Catatan guru untuk siswa ini.
- [ ] Layar progress saat model belum ada (UI untuk `DownloadState`)
- [ ] Layar progress saat inference berjalan (UI untuk `AiState`)
- [ ] Text box hasil narasi + tombol "Copy to Clipboard"
    - [ ] Text boxnya rich text box atau apa gitu jadi enak bisa langsung copy-paste ke word lengkap sama formattingnya.
- [ ] Penanganan error (model gagal load, download gagal, dll.) ditampilkan ke user

## 3. Premium Features (to be implemented setelah core functionality OK semua)

- [ ] Import data siswa dari Excel (.xlsx) — **DITUNDA**, lihat catatan di bawah
- [ ] Export hasil narasi ke Excel/Word secara bulk — **DITUNDA**, lihat catatan di bawah
- [ ] Export bulk ke Excel/Word


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