using System;
using System.Collections;
using System.Collections.Generic;
using BusinessEntities;
using DataModel;
namespace TestsHelper
{
  public class PatientComparer: IComparer, IComparer<PatientDetail>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as PatientDetail;
            var rhs = actual as PatientDetail;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(PatientDetail expected, PatientDetail actual)
        {
            int temp;
            return (temp = expected.DetailId.CompareTo(actual.DetailId)) != 0 ? 
                temp : expected.PatientData.CompareTo(actual.PatientData) !=0 ?
                temp : expected.Created.CompareTo(actual.Created)           
               ;
        }
    }
}
