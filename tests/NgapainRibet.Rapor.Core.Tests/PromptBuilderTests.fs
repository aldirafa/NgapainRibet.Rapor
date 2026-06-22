module NgapainRibet.Rapor.Core.Tests.PromptBuilderTests

open Xunit
open NgapainRibet.Rapor.Core.DomainModels
open NgapainRibet.Rapor.Core.PromptBuilder

let private sampleStudent =
    { Name = "Siti Aminah"
      Strengths = [ "Aljabar"; "Geometri" ]
      Weaknesses = [ "Statistika" ]
      Tone = "Memotivasi"
      Notes = "Siti sangat aktif di kelas dan selalu bersemangat dalam belajar." }

[<Fact>]
let ``buildSystemPrompt menyertakan nama mata pelajaran`` () =
    let prompt = buildSystemPrompt "Matematika" sampleStudent
    Assert.Contains("Matematika", prompt)

[<Fact>]
let ``buildSystemPrompt menyertakan tone yang dipilih`` () =
    let prompt = buildSystemPrompt "Matematika" sampleStudent
    Assert.Contains("Memotivasi", prompt)

[<Fact>]
let ``buildSystemPrompt menggabungkan beberapa Strengths dengan kata 'dan'`` () =
    let prompt = buildSystemPrompt "Matematika" sampleStudent
    Assert.Contains("Aljabar, dan Geometri", prompt)

[<Fact>]
let ``buildSystemPrompt menyertakan Weaknesses tunggal`` () =
    let prompt = buildSystemPrompt "Matematika" sampleStudent
    Assert.Contains("Statistika", prompt)

[<Fact>]
let ``buildSystemPrompt menangani Strengths kosong tanpa error`` () =
    let student = { sampleStudent with Strengths = [] }
    let prompt = buildSystemPrompt "Matematika" student
    Assert.Contains("belum ada Capaian Pembelajaran yang menonjol", prompt)

[<Fact>]
let ``buildSystemPrompt menangani Weaknesses kosong tanpa error`` () =
    let student = { sampleStudent with Weaknesses = [] }
    let prompt = buildSystemPrompt "Matematika" student
    Assert.Contains("belum ada area yang memerlukan bimbingan khusus saat ini", prompt)

[<Fact>]
let ``buildUserPrompt menyertakan nama siswa`` () =
    let prompt = buildUserPrompt sampleStudent None
    Assert.Contains("Siti Aminah", prompt)

[<Fact>]
let ``buildUserPrompt singkat dan to the point`` () =
    let prompt = buildUserPrompt sampleStudent None
    Assert.True(prompt.Length < 150, "User prompt seharusnya pendek — instruksi detail ada di system prompt")

[<Fact>]
let ``buildSystemPrompt menyertakan Student Notes ketika ada`` () =
    let prompt = buildSystemPrompt "Matematika" sampleStudent
    Assert.Contains("Siti sangat aktif di kelas", prompt)

[<Fact>]
let ``buildSystemPrompt tidak menyertakan baris Notes ketika kosong`` () =
    let student = { sampleStudent with Notes = "" }
    let prompt = buildSystemPrompt "Matematika" student
    Assert.DoesNotContain("Catatan tambahan dari guru", prompt)

[<Fact>]
let ``buildUserPrompt menyertakan additionalNotes ketika ada`` () =
    let prompt = buildUserPrompt sampleStudent (Some "fokus ke progress semester ini")
    Assert.Contains("fokus ke progress semester ini", prompt)

[<Fact>]
let ``buildUserPrompt tidak menambahkan apapun ketika additionalNotes None`` () =
    let withNone = buildUserPrompt sampleStudent None
    let withEmptyString = buildUserPrompt sampleStudent (Some "   ")
    Assert.Equal(withNone, withEmptyString)
