PROJECT SPECIFICATION & CONTEXT FOR AI AGENTS

Project Name: NgapainRibet.Rapor

Philosophy behind the name: Ngapain Ribet = “Why make simple things so complicated?” Rapor = report cards, basically an app that helps with report cards

Target Audience: Indonesian School Teachers (Guru SD/SMP/SMA)

Core Goal: An offline, local-AI-powered desktop application to generate personalized, narrative report card descriptions (Deskripsi Capaian Kompetensi Kurikulum Merdeka) without cloud API costs or internet dependency.

Project, code, and interface language should be in Bahasa Indonesia.

1. ARCHITECTURE OVERVIEW (NET 10.0 Polyglot)

The solution is split into two distinct projects within a single .NET 10.0 Solution to maximize development comfort, separation of concerns, and clean architecture:

NgapainRibet.Rapor.Core (F# Class Library)

Role: Backend engine, data processing, model downloading/caching logic, and AI integration.

Why F#: Immutability, robust data modeling (Discriminated Unions for states), safe async processing (task { ... }), and clean domain-driven logic.

Dependencies: LLamaSharp (or LLamaSharp.Backend.Cpu), System.Net.Http (for model downloading).

NgapainRibet.Rapor.UI (VB.NET Windows Forms - Target: WinForms for lightweight footprint)

Role: User interface, user event handling, configuration management, and presentation layer.

Why VB.NET: Rapid application development (RAD) for desktop tables/data grids, native Windows integration, and high familiarity for legacy-friendly enterprise-like systems.

Dependencies: Reference to NgapainRibet.Rapor.Core.

2. THE LOCAL AI ENGINE SPECIFICATIONS

To ensure the app runs smoothly on typical teacher laptops (often dual-core CPUs, 4GB to 8GB RAM, integrated graphics):

Execution Mode: CPU-only inference (via llama.cpp/LLamaSharp). No external GPU required.

Model Format: GGUF (Highly quantized, e.g., Q4_K_M).

Target Models:

Primary (Ultra-lightweight): Qwen-2.5-1.5B-Instruct-GGUF (approx. 1.2 GB download, highly capable in Indonesian).

Secondary (High-quality): Llama-3-8B-Indonesia-GGUF or Qwen-2.5-3B-Instruct-GGUF (for 8GB+ RAM systems).

Distribution Strategy: The main application executable is kept extremely small (~5-10MB). On first launch, the F# Core checks for the local .gguf file; if missing, it prompts the user to download it directly from HuggingFace to a local cache directory.

3. USER WORKFLOW (UX STEPS)

Step 1: Input Data

Teachers input student details (Name, Class) or import them via a standardized Excel file (.xlsx).

Step 2: Configuration & Settings

For each student, the teacher selects:

Subject (e.g., Matematika, Bahasa Indonesia).

Strengths (Capaian Tertinggi) via checklist of topics.

Weaknesses/Areas of Improvement (Capaian Terendah) via checklist of topics.

Tone setting (e.g., "Sangat Formal", "Memotivasi", "Tegas tapi Santun").

Step 3: Offline AI Inference

F# Core constructs a highly optimized system prompt dynamically based on the checklist selections.

LLamaSharp processes the prompt locally and streams/returns the output.

Step 4: Copy/Export

The generated narrative is displayed in a text box with a quick "Copy to Clipboard" button (for easy pasting into the government's official E-Rapor web portal) or exported in bulk back to Excel/Word.

4. DOMAIN MODELS & STATE MACHINE (F# Core Hint)

The core logic should model states safely. For example:

DownloadState: NotStarted | Downloading of float | Completed | Error of string

AiState: Uninitialized | LoadingModel | Ready | Generating | Failed of string

Student: { Name: string; Strengths: string list; Weaknesses: string list; Tone: string }

5. INSTRUCTIONS FOR THE AI ASSISTANT (YOU)

When helping with this project:

Adhere to the Language Split: Do not write UI logic in F#, and do not write AI inference/data parsing logic in VB.NET. Keep them strictly in their respective projects.

Clean Interoperability: Ensure that F# types (especially Async/Task operations and data structures) are exposed in a way that is easily consumable by VB.NET (using standard .NET Task, Nullable, or standard objects where necessary).

Optimized Prompting: Keep system prompts highly specific to Indonesian Kurikulum Merdeka standards (using terms like Capaian Pembelajaran, Bimbingan, Kompetensi).

Resource Consciousness: Write CPU-friendly LLamaSharp configuration code (low thread count context settings, small context window e.g., 512 - 1024 tokens to save RAM).