## SchemaGenerator 核心功能與架構摘要
- [回到 ReadMe](Readme.md "Readme")   
---  
SchemaGenerator 是一個專為解決不同資料庫結構文件格式轉換困難而設計的 **Create Table DDL 產生器**。

### 一、工具目的與核心機制 (Purpose & Mechanism)    

| 項目 | 說明與解決方案 | 來源 |
| :--- | :--- | :--- |
| **主要痛點** | 傳統上，遇到不同 Excel 格式的資料庫結構文件時，必須重複修改程式碼（如 Excel VAB）。 | |
| **核心目標** | 撰寫一個 **共用的程式**，透過外部設定檔來調整欄位對應。 | |
| **關鍵機制** | 採用 **抽象化與外部配置** 的機制，使程式碼與 Excel 欄位的實際物理位置解耦。 | |
| **配置依據** | 透過 **外部 JSON 設定檔** (Source Reader 設定檔)，利用 **欄位索引值 (IndexOf)**（起始值為 0）來進行欄位映射。 | |
| **靈活性** | 支援詳細的欄位配置，例如資料型別若分列 (Data Type 分成 2 欄) 可設定 `IndexOfDatatype01` 和 `IndexOfDatatype02`，以及定義 Primary Key (`IndexOfPK`)、Nullable (`IndexOfNullable`) 等。 | |

### 二、支援的輸入與輸出格式 (Supported Formats)

| 類型 | 項目 | 說明/名稱 | 來源 |
| :--- | :--- | :--- | :--- |
| **來源 (輸入) 格式** | 1. SDM 格式 | 包含 2 個 Worksheets：Table 列表一個，Column 資料全部在另一個。 | |
| | 2. TableSchema 格式 | 包含 1 個 Table 列表 Worksheet，以及 N 個獨立的 Column Schema Table Worksheet (共 1+N 個 Worksheets)。 | |
| **目的 (輸出) 格式** | 1. MSSQL | Microsoft SQL Server。 | |
| | 2. MSAzureSynapse | Microsoft Azure Synapse。 | |
| | 3. Oracle | | |
| | 4. MYSQL | My SQL。 | |
| | 5. POCO Model Class | 程式模型類別。 | |

### 三、擴充模組功能規範 (Extension Specification)

SchemaGenerator 採用模組化設計，支援擴充新的來源類型和 DDL 產生器。

#### 1. 來源文件類型 (Source Reader) 擴充規範
*   **專案命名規則：** `SourceReader.{TypeName}` (例如：SDM)。
*   **繼承規範：** 需繼承 `SourceReaderBase` 類別，並實作其方法。
*   **類別名稱規範：** `{TypeName}SourceReader`。
*   **配置檔：** 專案下需建立 `SourceReader.{TypeName}.json`，其中索引值 (Index) 從 "0" 開始計算。
*   **部署：** 建置後將 *.dll 和 *.pdb 檔案複製到主要程式專案目錄下的 `Extension` 資料夾。

#### 2. 產生 DDL DB 類型 (Schema Generator) 擴充規範
*   **專案命名規則：** `SchemaGenerator.{TypeName}` (例如：MSSQL、Oracle)。
*   **繼承規範：** 需繼承 `SchemaGeneratorBase` 類別，並實作其方法。
*   **類別名稱規範：** `{TypeName}SchemaGenerator`。
*   **部署：** 建置後將 *.dll 和 *.pdb 檔案複製到主要程式專案目錄下的 `Extension` 資料夾。

#### 3. 設定與範本路徑
*   **JSON 設定檔預設路徑：** `\TableSchemaGenerator\App_Data\*.json` 檔。
*   **Excel 範本預設路徑：** `\TableSchemaGenerator\Template\ Excel檔`。
*   **下拉選單顯示名稱：** 可在 `AppData.json` 中的 `SourceType` 或 `DatabaseTypes` 物件中設定 `Name` 欄位，以顯示更完整的說明名稱；若無設定，則顯示元件的 DDL Type 名稱。

### 四、打破自我局限-未來功能 (Future & Feature)
- Persona：
  - 一般行政、文書人員
  - 其他有用較穩定格式記錄資料，想轉換成其他報表或表格文件輸出
  - 其他您提供需求

