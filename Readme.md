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


