SoundOparator�̎g�p���@

Setup�ɂ���

1. Hierarchy��� Asset/Utility/Sounds/Prefabs �ɂ���v���t�@�u��z�u
2. SoundOperator��DataBase���A�^�b�`����B

Request�ɂ���

�Q�l => �T���v����User���Q��




SoundOperater.PlayRequester�ɂ���

�K�{�L�q
SetPath(�T�E���h�̃f�[�^�p�X); 
SetType(�T�E���h�̃f�[�^�^�C�v);
Request();�@�f�[�^���Z�b�g������ŌĂяo���B

�C�ӋL�q
SetUser(�g�p��); �g�p�҂̗L����ݒ�





SoundOperater.StopRequester�ɂ���

�K�{�L�q
CreateData(); �f�[�^�̃C���X�^���X���쐬
Request(); �f�[�^���Z�b�g������ŌĂяo���B

�C�ӋL�q
CurrentBGM(); ���ݍĐ�����BGM���~�߂�B
SetPath(�T�E���h�̃f�[�^�p�X); �w�肵��Sound���~�߂�B
SetAll(); �S�Ă�Sound���~�߂�B