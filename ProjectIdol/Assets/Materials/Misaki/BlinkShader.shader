Shader "Custom/BlinkShader" // �V�F�[�_�[�̖��O���`�@"�O���[�v��/�V�F�[�_�[��"
{
    // ���������}�e���A���œ��͂ł��鍀��
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {} // �X�v���C�g�̃e�N�X�`��
        _BlinkSpeed ("Blink Speed", Range(0, 10)) = 1 // �_�ő��x�̃p�����[�^
        _BlinkIntensity ("Blink Intensity", Range(0, 1)) = 1 // �_�ł̋��x�̃p�����[�^
        _TransparencyMin ("TransparencyMin", Range(0,1)) = 0.1 // �_�ł����Ȃ����l
    }
    // ���̃V�F�[�_�[�̎�ȏ���
    SubShader
    {
        // �^�O��ݒ�
        Tags
        {
            "RenderType"="Transparent" // �����_�����O�^�C�v�𓧖��ɐݒ�
            "Queue"="Transparent"
           // "RenderPipeline"="UniversalPipeline" // �����_�[�p�C�v���C����URP�ɑΉ�
        }

        // �u�����h�ݒ��ǉ�
        Blend SrcAlpha OneMinusSrcAlpha
        
        // [Level Of Detail] �`�ʃ��x���@
        LOD 100 // LOD�l��100�ȏ�Ȃ炱�̃}�e���A����`��

        // 1�̃I�u�W�F�N�g��1�x�̕`��ōs������
        Pass
        {
            CGPROGRAM // �V�F�[�_�[��Cg�R�[�h�u���b�N�̊J�n
            #pragma vertex vert // ���_�V�F�[�_�[�֐��̎w��
            #pragma fragment frag // �t���O�����g�V�F�[�_�[�֐��̎w��
            #include "UnityCG.cginc" // Unity�̋��ʃV�F�[�_�[�C���N���[�h�t�@�C����ǂݍ���

            // Unity����󂯎��f�[�^�\����
            struct appdata_t
            {
                float4 vertex : POSITION; // ���_�̈ʒu
                float2 uv : TEXCOORD0; // �e�N�X�`�����W
            };

            // �t���O�����g�V�F�[�_�[�Ŏg�p����\����
            struct v2f
            {
                float2 uv : TEXCOORD0; // �t���O�����g�V�F�[�_�[�ɓn���e�N�X�`�����W
                float4 vertex : SV_POSITION; // �N���b�v��Ԃł̒��_�̈ʒu
            };

            sampler2D _MainTex; // ���C���e�N�X�`���̃T���v���[
            float4 _MainTex_ST; // �e�N�X�`�����W�̕ϊ��s��
            float _BlinkSpeed; // �_�ő��x�̕ϐ�
            float _BlinkIntensity; // �_�ŋ��x�̕ϐ�
            float _TransparencyMin; // �_�ł����Ȃ����l


            // Unity����擾�����f�[�^��ϊ�����֐�(���_�V�F�[�_�[�Ɋւ���֐�)
            v2f vert (appdata_t v)
            {
                v2f o; // v2f�\���̂𐶐�
                o.vertex = UnityObjectToClipPos(v.vertex); // ���_�ʒu���N���b�v��Ԃɕϊ�
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // �e�N�X�`�����W��ϊ�
                return o;
            }

            // Unity����擾�����f�[�^���t���O�����g�ɔ��f������֐�(�t���O�����g�V�F�[�_�[�Ɋւ���֐�)
            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv); // �e�N�X�`������F���T���v�����O
                if (col.a < _TransparencyMin) // �A���t�@�l���Ⴂ�����i���������j�ɂ͓_�ł�K�p���Ȃ�
                {
                    col.rgb = (0, 0, 0);
                    col.a = 0;
                    return col; // ���̂܂܂̐F��Ԃ�
                }
                float blink = abs(sin(_Time.y * _BlinkSpeed)) * _BlinkIntensity; // �_�ł̌v�Z _Time��Unity�������I�ɒ񋟂��鎞�Ԃ̃p�����[�^
                                                                                 // _Time.x...�Q�[�����Ԃ̕b�� .y...�b��2�{ .z...3�{ .w...4�{
                col.rgb *= (1.0 - blink); // �_�ł�K�p (���������ɂ͉e�����Ȃ�)
                col.a = 1;
                return col; // �v�Z���ʂ̐F��Ԃ�
            }
            ENDCG // �V�F�[�_�[��Cg�R�[�h�u���b�N�̏I��
        }
    }
    FallBack "Transparent" // �V�F�[�_�[���T�|�[�g����Ȃ��ꍇ�Ɏg�p����t�H�[���o�b�N�V�F�[�_�[
}