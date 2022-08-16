using System.Collections.Generic;
using System.Linq;
using Data;

namespace Controller
{
    public class FindDublicateAlgoritm
    {
        
       public List<List<ElementData>> GetMatches(List<ElementData> datalist, int h, int w)
        {

            var YVer = h;
            var xHor = w;

            var goalInLineV = new List<List<List<ElementData>>>();
            for (var y = 0; y < YVer; y++)
            {
                var line = datalist.Where(e => e.yPos == y).OrderBy(i=>i.xPos).ToList();//! y = hor, x = ver
                var newLine = GetListIdsInLine(line);
               
                if (newLine.Count > 0)
                    goalInLineV.Add(newLine);
            }
           
            var goalInLineH = new List<List<List<ElementData>>>();
            
            for (var x = 0; x < xHor; x++)
            {
                var line = datalist.Where(e=>e.xPos == x).OrderBy(i=>i.yPos).ToList();
                var newLine = GetListIdsInLine(line);
                
                if (newLine.Count > 0)
                    goalInLineH.Add(newLine);
            }

            var result = new List<List<ElementData>>();
            
            foreach (List<List<ElementData>> l2 in goalInLineH.Concat(goalInLineV))
            //foreach (List<List<ElementData>> l2 in goalInLineV)
            {
                foreach (List<ElementData> l1 in l2)
                {
                    //foreach (var el in l1)
                    //{
                        if (!result.Contains(l1))
                        {
                            result.Add(l1);
                        }
                    //}
                }
                
            }
            
            return result;
            
        }
// public List<List<ElementData>> GetMatches(ElementData[,] elementDatas)
//         {
//
//             var YVer = elementDatas.GetLongLength(0);
//             var xHor = elementDatas.GetLongLength(1);
//
//             var goalInLineV = new List<List<List<ElementData>>>();
//             for (var y = 0; y < YVer; y++)
//             {
//                 ElementData[] line = new ElementData[xHor];
//
//                 for (var x = 0; x < xHor; x++)
//                 {
//                     line[x] = elementDatas[y, x]; //! y = hor, x = ver
//                 }
//
//                 var newLine = GetListIdsInLine(line);
//                 if (newLine.Count > 0)
//                 {
//                     goalInLineV.Add(newLine);
//                 }
//             }
//            
//             var goalInLineH = new List<List<List<ElementData>>>();
//             
//             for (var x = 0; x < xHor; x++)
//             {
//                 ElementData[] line = new ElementData[YVer];
//             
//                 for (var y = 0; y < YVer; y++)
//                 {
//                     line[y] = elementDatas[y, x];
//                 }
//             
//                 var newLine = GetListIdsInLine(line);
//                 if (newLine.Count > 0)
//                 {
//                     goalInLineH.Add(newLine);
//                 }
//             }
//
//             List<int> f = new List<int>();
//             List<int> g = new List<int>();
//             var ge = g.Concat(f);
//
//
//             var result = new List<List<ElementData>>();
//             
//             foreach (List<List<ElementData>> l2 in goalInLineH.Concat(goalInLineV))
//             //foreach (List<List<ElementData>> l2 in goalInLineV)
//             {
//                 foreach (List<ElementData> l1 in l2)
//                 {
//                     //foreach (var el in l1)
//                     //{
//                         if (!result.Contains(l1))
//                         {
//                             result.Add(l1);
//                         }
//                     //}
//                 }
//                 
//             }
//             
//             return result;
//             
//         }

        private static List<List<ElementData>> GetListIdsInLine(IList<ElementData> list)
        {
            List<List<ElementData>> result = new List<List<ElementData>>();
            List<ElementData> curL = new List<ElementData>();


            for (int i = 1; i < list.Count; i++)
            {
                if (list[i - 1].type == list[i].type)
                {
                    if (curL.Count == 0)
                    {
                        curL.Add(list[i - 1]);
                        curL.Add(list[i]);
                    }
                    else
                    {
                        curL.Add(list[i]);
                    }
                }
                else
                {
                    if (curL.Count > 2)
                    {
                        result.Add(curL);
                    }

                    curL = new List<ElementData>();
                }
            }

            if (curL.Count > 2)
            {
                result.Add(curL);
            }

            return result;
        }
    }
}