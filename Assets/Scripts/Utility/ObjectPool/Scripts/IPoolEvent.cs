namespace ObjectPool
{
    /// <summary>
    /// PoolObject���C�ӂ̃^�C�~���O�ŏI���𔻒肵�����ꍇ�Ɍp��
    /// </summary>
    public interface IPoolEvent
    {
        /// <summary>
        /// Event�̏I���^�C�~���O��True��Ԃ��悤�ɂ���
        /// </summary>
        bool IsDone { get; set; }
    }
}