這是一份根據您提供的 GitHub 原始資料，以輕鬆偏正式風格重新撰寫的 [`ReadMe_vOld.md`](ReadMe_vOld.md "Previous version ReadMe") 文件。

---

# SchemaGenerator: 高彈性資料表 DDL 產生器 (Create Table DDL SQL 工具)

SchemaGenerator 是一個專為解決不同格式資料庫結構文件轉換痛點而設計的工具。我們讓 DDL 產生過程變得輕鬆簡單，不再需要為了不同的 Excel 格式文件而反覆修改程式碼！

## 摘要介紹
- [SchemaGenerator 大網摘要](SchemaGenerator_IntroductionSummary.md "大網摘要")

- [語音介紹](https://drive.google.com/file/d/1egU3p7SK6OPbEub5MimTBYHx8zhsMkLX/view?usp=drive_link "語音介紹")


## 一、環境背景 (Context & Background)

### 1. 使用者情境介紹：告別 VAB，擁抱配置化生成！

在專案開發過程中，資料庫結構文件常以 Excel 格式儲存。然而，當您跨越不同的專案時，往往會遇到 **不同格式的 Excel** 規範，如果採用傳統的 Excel VBA 自動產生 DDL SQL 方式，就必須為了每一種新格式而修改一次 VBA。

**SchemaGenerator 的核心價值** 在於提供一個「共用的程式」，透過外部設定檔來動態調整欄位對應，從根本上讓程式碼與 Excel 欄位的實際物理位置解耦。

*   **目標使用者群 (Persona)：**
    *   **SA/SD (系統分析/設計師)：** 快速將設計好的 Excel 規格轉換為資料定義語言 (DDL)。
    *   **DBM/DBA (資料庫管理員/架構師)：** 高效率地根據多樣化的外部文件格式，生成並部署資料表。
    *   **專案開發成員：** 確保從規格文件到程式碼模型 (例如 POCO Model Class) 的轉換過程標準化且低錯誤。

總結來說，此工具特別適用於 **需要依賴 Excel 格式文件來建立資料表的人員**。

### 2. 潛在應用情境：從資料定義到泛用格式轉換機

由於 SchemaGenerator 採用了高度可擴充的架構，並透過 **抽象化與外部配置** 的機制來處理資料來源格式，其潛力並不僅限於 DDL 的產生：

*   **情境擴展：** 任何來源為 **Excel 或其他有固定格式的文件檔**，若目標是轉換成共用格式的報表或其他結構化輸出時，都可以透過擴充 Source Reader 和 Schema Generator 模組來達成。
*   **潛在使用者群 (Persona)：**
    *   **一般行政文書處理人員：** 處理大量、但格式相對固定的例行性文書作業，例如將複雜的表單數據自動轉換成標準化的 JSON、CSV 或其他報表格式。

藉由 JSON 設定檔的彈性配置，本工具能利用欄位索引值 (IndexOf) 進行欄位映射，使程式具備處理各種固定格式數據的潛能。

---

## 二、功能說明 (Feature Overview)

SchemaGenerator 的核心功能是高效且靈活地從結構文件生成 DDL (Data Definition Language) SQL 或模型程式碼。

### 1. 配置驅動的資料來源彈性處理

*   **解決痛點：** 程式不需修改，僅需調整 **外部 JSON 設定檔** (Source Reader 設定檔) 即可適應不同的 Excel 結構。
*   **關鍵機制：** 程式利用 **欄位索引值 (IndexOf)** 來告訴程式，資料元素在 Excel 工作表中的具體位置（索引值從 0 開始計算）。
    *   例如，不論「資料表名稱」在 Excel 的 A 欄 (Index 0) 還是 D 欄 (Index 3)，都可以透過修改設定檔中的 `IndexOfTableName` 值來適應。
    *   設定檔中甚至考慮到某些文件會將 **資料型別 (Data Type)** 分成兩欄的特殊情況，提供 `IndexOfDatatype01` 和 `IndexOfDatatype02` 兩個索引配置。
*   **支援的來源格式 (目前已支援兩種主要 Excel 類型)：**
    *   **SDM 格式：** 包含 2 個 Worksheets，分別是 Table 列表和 Column 資訊。
    *   **TableSchema 格式：** 包含 1 個 Table 列表 Worksheet，以及 N 個獨立的 Column Schema Table Worksheet (1+N 個 Worksheets)。

### 2. 多元化的 DDL 與模型輸出能力

本工具不僅限於單一資料庫，目前已支援多種主流資料庫和程式模型輸出類型：

*   **資料庫 DDL 輸出：**
    1.  **MSSQL：** Microsoft SQL Server。
    2.  **MSAzureSynapse：** Microsoft Azure Synapse。
    3.  **Oracle**。
    4.  **MYSQL：** My SQL。
*   **程式模型輸出：**
    5.  **POCO Model Class**。

### 3. 設定與範本管理

*   **預設檔案路徑：** 預設的 JSON 設定檔位於 `\TableSchemaGenerator\App_Data\*.json` 路徑。Excel 範本檔則位於 `\TableSchemaGenerator\Template\ Excel檔` 路徑。
*   **AppData.json：** 這個設定檔用於定義資料來源類型 (`SourceType`) 和資料目的類型 (`DatabaseTypes`)，並提供完整的描述名稱，讓前端下拉選單能自動對應並顯示，若無設定則顯示元件的 DDL Type 名稱。

---

## 三、擴充模組功能規範 (Extension Module Specification)

SchemaGenerator 具備高度的模組化架構，允許開發者輕鬆擴充新的輸入來源類型或輸出 DDL 類型。所有擴充模組的 DLL 和 PDB 檔案在開發完成後，都應複製到主要程式專案目錄下的 `Extension` 資料夾，供程式自動判斷並載入。

### 1. 來源文件類型擴充 (Source Reader)

如果您遇到新的 Excel 格式或想整合其他結構化文件，可以透過擴充 Source Reader 模組來達成：

*   **專案建立規範：** 新類別專案命名規則為：`SourceReader.{TypeName}` (例如，{TypeName}可以是自定義的類型代碼，如 SDM、TableSchema 等)。
*   **類別繼承與實作：** 需參考 `SchemaGenerator.BaseFactory` 專案，建立類別並繼承 **`SourceReaderBase` 類別**，並實作其方法。類別名稱應規範為 `{TypeName}SourceReader`。
*   **配置檔設定：**
    *   在專案下建立 `App_Data` 資料夾，並建立名為 `SourceReader.{TypeName}.json` 的設定檔。
    *   **注意：** JSON 設定檔中的欄位索引值 (Index) 必須由 **"0"** 開始計算。如果特定格式（如 SDM）的欄位順序有差異，使用者可以自行修改對應的 `*.json` 檔案。

### 2. 產生 DDL 資料庫類型擴充 (Schema Generator)

如果您需要支援一個新的資料庫類型 (例如 Teradata 或 PostgreSQL) 或客製化的輸出格式，可以透過擴充 DDL DB 類型模組來達成：

*   **專案建立規範：** 新類別專案命名規則為：`SchemaGenerator.{TypeName}` (例如，{TypeName}可以是 MSSQL、Oracle 等)。
*   **類別繼承與實作：** 需參考 `SchemaGenerator.BaseFactory` 專案，建立類別並繼承 **`SchemaGeneratorBase` 類別**，並實作其方法。類別名稱應規範為 `{TypeName}SchemaGenerator`。

---

## 四、打破自我局限-未來功能 (Future & Feature)
- Persona：
  - 一般行政、文書人員
  - 其他有用較穩定格式記錄資料，想轉換成其他報表或表格文件輸出
  - 其他您提供需求

---

## 工具下載

* Win x64 可攜式(23MB)(google doc)：[TableSchemaGenerator_v3.0.zip](https://drive.google.com/file/d/1OKg_0T7pyAeFG_WvDgaMpqtcau_t1mZC/view?usp=sharing) 
