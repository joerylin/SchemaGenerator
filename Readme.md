# Table Schema Generator (Create Table DDL SQL �u��)
## �ݨD����
#### �@�B�]�M�׸�Ʈw���c���Excel�A�H�e�ߺD��Excel VAB�g�۰ʲ��� Create Table DDL SQL�A�]�ثe��L�M�צ����PExcel�榡�N�n��@��VBA�C�]���g�@�Ӧ@�Ϊ��{���A�z�L�]�w�ɨӽվ����
* �u��U���G[tmpfile\TableSchemaGenerator_v3.0.zip](https://drive.google.com/drive/folders/19AGdzsSI24Sqqg_xNU_yWAR5p4IYc3FJ "tmpfile\TableSchemaGenerator_v3.0.zip")

#### �G�B�ثe�䴩���榡
* �ӷ��G�ثe�䴩2�ؤ��PExcel�榡  
    1. SDM�GTable�C��@��Worksheet�AColumn�]�����b�@��worksheet�A�@ 2 ��Worksheets�F(��ƪ�W��Worksheet)
    2. TableSchema�GTable�C��@��Worksheet�AColumn Schema�U��Table �U�ۤ@��workshee�A�@ 1+ N(Column Schema) ��Worksheets�F(��ƪ�U��Worksheet)
* �ت��G
    1. MSSQL�GMicrosoft SQL Server
    2. MSAzureSynapse�GMicrosoft Azure Synapse
    3. Oracle
    4. MYSQL�GMy SQL


## �����d���γ]�w�ɻ���
#### �@�B�d����  
Template���|�G  
* (1) �w�]*.json�ɸ��|�G\TableSchemaGenerator\App_Data\*.json��  
      i.  [SDM�榡����(��ƪ�W��Worksheet)](TableSchemaGenerator\App_Data\SourceReader.SDM_Defalut.json)  
      ii. [Table Schema�榡����(��ƪ�U��Worksheet)](TableSchemaGenerator\App_Data\SourceReader.TableSchema.json) 
* (2) Excel �ɡG\TableSchemaGenerator\Template\ Excel��  
      i.  [SDM�榡����(��ƪ�W��Worksheet)](TableSchemaGenerator\Template\SDM_MIS.xlsx)  
      ii. [Table Schema�榡����(��ƪ�U��Worksheet)](TableSchemaGenerator\Template\TableSchema.xls)

#### �G�BAppData.json �]�w��
1. �Y�ϥγ]�w�ɡA�U�Կ��|�۰ʹ����]�w��
2. �Y�L�]�w/�����A�Y��ܤ���ddl Type�W��

![AppData.json](tmpfile/AppData_josn.png)

 | ���� | ��ƫ��O | ���� | �Ƶ� |
| ----------------------- | ------------------ |---------------------------- | ----------------------------- |
| SourceType | object[] | ��ƨӷ����� | |
| DatabaseTypes | object[] | ��ƥت����� | |
|  | object[] | ��ƨӶ��ت���w�q | |
| Type | string | ��ƨӶ��B�ت������� | ex:SDM�BTableSchema�BMSSQL...etc�C�����P����{typename}�ۦP |
| Name | string | �Ψӻ������W�٩δy�z |  |

#### �T�BSource Reader json �]�w��
    1. SourceRead*.josn�G�]�w��ƨӷ��榡  
        �]�w�ɩR�W�W�h���G��SourceReader.{TypeName}_{Name}.json��
        i.  {TypeName}�G�����N�X ex: SDM�BTableSchema      
        ii. {Name}�G�۩w�q�ϧO���W��
    
    2.  �@���Ƴ]�w

 | ���� | ��ƫ��O | ���� | �Ƶ� |
| --------------------------- | -------------- |----------------------- | ---------------------------------- |
| DataSourceType | String | ��ƨӷ����� | ex:SDM�BTableSchema |
| TableSummaryWorksheet | String | ��ƪ�C�� worksheet name | ex:SDM-Summary |    
| TableSchemaWorksheet | String | ��ƪ���� worksheet name | ex:SDM-Detail |

    3.  ��ƪ��T�]�w
![Table Setting](tmpfile/SourceReader_json_config01.png)

 | ���� | ��ƫ��O | ���� | �Ƶ� |
| --------------------------- | -------------- |----------------------- | ---------------------------------- |
| TableConfiguration | Object | ��ƪ�����]�w��� | �U�CIndexOf���Y�L�]��null |
| IndexOfStartRow | Integer | ��ư_�l�C�A��0�}�l |  |
| IndexOfSeq | Integer | �Ǹ������ޭ� |  |
| IndexOfArea | Integer | �D�n���������ޭ� |  |
| IndexOfSubjectArea | Integer | �l���������ޭ� |  |
| IndexOfDataBase | Integer | ��Ʈw�����ޭ� |  |
| IndexOfSchema | Integer | ��Ƶ��c�����ޭ� |  |
| IndexOfTableName | Integer | ��ƪ�W�������ޭ� |  |
| IndexOfTableComment | Integer | ��ƪ��������ޭ� |  |
| IndexOfTableOriginalName | Integer | ��ƪ��l�W�������ޭ� |  |
| IndexOfTableDescription | Integer | ��ƪ��L�y�z�����ޭ� |  |
| ExtensionData | Object | ��L�X�R����T�� |  |
| Key | String | �X�R������ | �^��R�W |
| Name | String | �W�� |  |
| IndexOfColumn | Integer | �X�R�����ޭ� |  |

    4.  ����Ƴ]�w
![Column Setting](tmpfile/SourceReader_json_config02.png)

 | ���� | ��ƫ��O | ���� | �Ƶ� |
| --------------------------- | -------------- |----------------------- | ---------------------------------- |
| SchemaConfiguration | Object | ��ƪ������]�w��� | �U�CIndexOf���Y�L�]��null |
| IndexOfStartRow | Integer | ��ư_�l�C�A��0�}�l |  |
| IndexOfColumnSeq | Integer | �Ǹ������ޭ� |  |
| IndexOfArea | Integer | �D�n���������ޭ� |  |
| IndexOfSubjectArea | Integer | �l���������ޭ� |  |
| IndexOfDataBase | Integer | ��Ʈw�����ޭ� |  |
| IndexOfSchema | Integer | ��Ƶ��c�����ޭ� |  |
| IndexOfTableName | Integer | ��ƪ�W�������ޭ� |  |
| IndexOfColumnName | Integer | ������W�������ޭ� |  |
| IndexOfDatatype01 | Integer | ��ƫ��O�����ޭ�-01 |  |
| IndexOfDatatype02 | Integer | ��ƫ��O�����ޭ�-02 | ���Ǥ��|�NData Type ����2��N�Ψ�-02 |
| IndexOfColumnDescription | Integer | �����y�z�����ޭ� |  |
| IndexOfPK | Integer | Primary Key�����ޭ� |  |
| IndexOfFK | Integer | Foreigner Key�����ޭ� |  |
| IndexOfNullable | Integer | �O�_�ह�\Null Value�����ޭ� | Null �P Not Null ���G�ܤ@ |
| IndexOfNotNull | Integer | �O�_��Not Null �����ޭ� |  |
| IndexOfColumnOriginalName | Integer | ������l�W�������ޭ� |  |
| ExtensionData | Object | ��L�X�R����T�� |  |
| Key | String | �X�R������ | �^��R�W |
| Name | String | �W�� |  |
| IndexOfColumn | Integer | �X�R�����ޭ� |  |




# DDL ���;� �{���\�� �X�R�W�d
## �@�B�ӷ��������
    1. ���X�R�s�����\��ɡA�إ߷s���O�M�שR�W�W�h���G��SourceReader.{TypeName}��
        i. {TypeName}�G�����N�X ex: SDM�BTableSchema

    2. �ݰѦҡySchemaGenerator.BaseFactory�z�M��
         i. �إ����O�B�~�ӡySourceReaderBase�z���O�A�ù�@���k
        ii. ���O�W�٩R�W�W�d��{TypeName}SourceReader��

    3. �M�פU�إߡyApp_Data�z��Ƨ��A�ëإߡ�SourceReader.{TypeName}.json��
        i. {TypeName}�G�����N�X ex: SDM�BTableSchema
        ii. *.json�ɮ��ݩʳ]�w�G�ظm�ʧ@=�L�F�ƻs���X�ؿ�=�û��ƻs
        iii. ps:�t�Y��SDM�榡�A��춶�Ǧ��t���Цۤv�ק� *.json�ɡA�Y�����Υi�ۤv�ƥ��A�{���u�|Ū���W�z�R�W�W�h��*.json��
        iv. *.json�]�w�ɸ̪� index �_�l�ȥ�"0"�}�l   

    4. �}�o����ظm��N*.dll<���n>�B*.pdb�ƻs��D�n�{���M�ץؿ��U�yExtension�z��Ƨ��U�A�ѵ{���۰ʧP�_�}���X�R

    5. �p�ݭn�U�Կ����ܮɦ��󧹾�W�٩λ����A�Цb�yApp_Data�z��Ƨ��U��AppData.json���ɡA
        �}�ҽs��ó]�w��SourceTypes�������ơA�Y�L���]�w�Y���ddl Type�W��
            
    6. ��ƨӷ��ɮסA�Y�L�q�e�ݿ���Aex: Excel SDM file :SDM_MIS.xlsx�w�]�������{���ڥؿ��U���yTemplate�z�ؿ��U�A

## �G�B����DDL DB����
    1. ���X�R�s�����\��ɡA�إ߷s���O�M�שR�W�W�h���G��SchemaGenerator.{TypeName}��
        i. {TypeName}�G�����N�X ex: MSSQL�BOracle�BTeradata
    2. �ݰѦҡySchemaGenerator.BaseFactory�z�M�� 
         i. �إ����O�B�~�ӡySchemaGeneratorBase�z���O�A�ù�@���k
        ii. ���O�W�٩R�W�W�d��{TypeName}SchemaGenerator��
    3. �}�o����ظm��N*.dll<���n>�B*.pdb�ƻs��D�n�{���M�ץؿ��U�yExtension�z��Ƨ��U�A�ѵ{���۰ʧP�_�}���X�R



