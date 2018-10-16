using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resolver
{
   public interface IComponent
    {
       void SetUp(IRegisterComponent registerComponent);
    }
}
