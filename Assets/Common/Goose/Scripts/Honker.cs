using _Plugins.TopherUtils;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using UnityEngine;

namespace Common.Goose.Scripts
{
    public class Honker : Goose
    {
        private Vector3 _targetDestination;

        
        private   bool    ReachedDestination() => Vector3.Distance(transform.position, _targetDestination) <= 3f;

        private Vector3 FindNewDestination()
        {
            return new Vector3(Random.Range(-10f, 10f), 0f, (Random.Range(-10f, 10f)));
        }

        protected override BehaviorTree CreateBehaviorTree()
        {
            return new BehaviorTreeBuilder(gameObject)
                  .Sequence()
                  .Parallel()
                  
                  .Sequence("Stunned")
                  .Condition("Stunned", Stunned)
                  .Wait()
                  .End()
                   
                   .Sequence("Movement")
                  .Condition("Not Held", () => !_held)
                  .Condition("Not eating", () => !_eating)
                   .Do("Move", () =>
                   {
                       var dir      = _targetDestination - transform.position;
                       var nextMove = new Vector2(dir.x, dir.z).normalized;
                       _motor.Move(nextMove * _speed);
                       
                       return ReachedDestination() ? TaskStatus.Success : TaskStatus.Continue;
                   })
                   
                  .Do("Find new target", () =>
                  {
                      // TODO: Finish me
                      _targetDestination = FindNewDestination();
                      _speed             = _speedRange.RandomValue();
                      return TaskStatus.Success;
                  })
                   .End()

                            
                  .Build();
        }
    }
}