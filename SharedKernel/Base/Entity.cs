using Yitter.IdGenerator;

namespace SharedKernel.Base
{
    public abstract class Entity
    {
        public long Id { get; protected set; }

        protected Entity()
        {
            Id = YitIdHelper.NextId();
        }
    }
}