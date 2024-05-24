# DDL 產生器 擴充規範
## 一、來源文件類型
    1. 需擴充新類型功能時，建立新類別專案命名規則為：〝SourceReader.{TypeName}〞
        i. {TypeName}：類型代碼 ex: SDM、TableSchema

    2. 需參考『SchemaGenerator.BaseFactory』專案
         i. 建立類別且繼承『SourceReaderBase』類別，並實作其方法
        ii. 類別名稱命名規範〝{TypeName}SourceReader〞

    3. 專案下建立『App_Data』資料夾，並建立〝SourceReader.{TypeName}.json〞
        i. {TypeName}：類型代碼 ex: SDM、TableSchema
        ii. *.json檔案屬性設定：建置動作=無；複製到輸出目錄=永遠複製
        iii. ps:另若該SDM格式，欄位順序有差異請自己修改 *.json檔，若有不用可自己備份，程式只會讀取上述命名規則的*.json檔
        iv. *.json設定檔裡的 index 起始值由"0"開始   

    4. 開發完後建置後將*.dll<必要>、*.pdb複製於主要程式專案目錄下『Extension』資料夾下，供程式自動判斷開啟擴充

    5. 如需要下拉選單顯示時有更完整名稱或說明，請在『App_Data』資料夾下〝AppData.json〞檔，
        開啟編輯並設定〝SourceTypes〞物件資料，若無此設定即顯示ddl Type名稱
            
    6. 資料來源檔案，若無從前端選取，ex: Excel SDM file :SDM_MIS.xlsx預設須放執行程式根目錄下的『Template』目錄下，

## 二、產生DDL DB類型
    1. 需擴充新類型功能時，建立新類別專案命名規則為：〝SchemaGenerator.{TypeName}〞
        i. {TypeName}：類型代碼 ex: MSSQL、Oracle、Teradata
    2. 需參考『SchemaGenerator.BaseFactory』專案 
         i. 建立類別且繼承『SchemaGeneratorBase』類別，並實作其方法
        ii. 類別名稱命名規範〝{TypeName}SchemaGenerator〞
    3. 開發完後建置後將*.dll<必要>、*.pdb複製於主要程式專案目錄下『Extension』資料夾下，供程式自動判斷開啟擴充
    5. 如需要下拉選單顯示時有更完整名稱或說明，請在『App_Data』資料夾下〝AppData.json〞檔，
        開啟編輯並設定〝DatabaseTypes〞物件資料，若無此設定即顯示ddl Type名稱


