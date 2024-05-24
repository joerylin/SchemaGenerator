# DDL ���;� �X�R�W�d
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
    5. �p�ݭn�U�Կ����ܮɦ��󧹾�W�٩λ����A�Цb�yApp_Data�z��Ƨ��U��AppData.json���ɡA
        �}�ҽs��ó]�w��DatabaseTypes�������ơA�Y�L���]�w�Y���ddl Type�W��

## �T�BSource Reader json �]�w��
    1.  �@���Ƴ]�w

 | ���� | ��ƫ��O | ���� | �Ƶ� |
| --------------------------- | -------------- |----------------------- | ---------------------------------- |
| DataSourceType | String | ��ƨӷ����� | ex:SDM�BTableSchema |
| TableSummaryWorksheet | String | ��ƪ�C�� worksheet name | ex:SDM-Summary |    
| TableSchemaWorksheet | String | ��ƪ���� worksheet name | ex:SDM-Detail |

    2.  ��ƪ��T�]�w
![Table Setting](picture/SourceReader_json_config01.png)

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

    3.  ����Ƴ]�w
![Column Setting](picture/SourceReader_json_config02.png)

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
