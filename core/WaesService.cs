using System;
using System.Collections.Generic;

namespace Waes.Core 
{
    /// <summary>
    /// Business Rules Entity Service
    /// </summary>
    public class WaesService
    {
        /// <summary>
        /// Saves or updates an Entity
        /// </summary>
        /// <param name="ett">Entity to be saved or updated </param>
        /// <returns> Operation successfull or failure </returns>
            public static bool SaveOrUpdate(Entity ett)
            {
                Validate(ett);

                try
                {
                    var ctx = new WaesContext();
                    var ent = ctx.Entities.Find(ett.Id);
                    if(ent != null)
                    {
                        ent.Left = String.IsNullOrEmpty(ett.Left) ? ent.Left: ett.Left;
                        ent.Right = String.IsNullOrEmpty(ett.Right) ? ent.Right: ett.Right;
                    }
                    else
                    {
                        ctx.Add(ett);
                    }
                    
                    return ctx.SaveChanges() > 0;

                    // Func<Entity, int> save = (e) => { ctx.Add(e);return ctx.SaveChanges(); };
                    // Func<Entity, int> update = (e) => { ctx.Update(e);return ctx.SaveChanges(); };
                    // Dictionary<bool,Func<Entity,int>> process = 
                    // new Dictionary<bool, Func<Entity, int>>() { {true, update}, {false, save} };
                                        
                    // return process[ent == null](ett) > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" === ERROR WHILE SAVING ENTITY: { ex.Message }");
                    return false;
                }
                
            }

    /// <summary>
    /// Search an entity by its id
    /// </summary>
    /// <param name="id">An integer that represents the Entity Id</param>
    /// <returns>An entity with the given Id if exists, otherwise null</returns>
        public static Entity Get(int id) => new WaesContext().Entities.Find(keyValues: id);

        /// <summary>
        /// Validates an Entity, throwing and exception if the Id is invalid or both properties Left and Right are empty
        /// </summary>
        /// <param name="ett">The Entity to be validated</param>
        private static void Validate(Entity ett) 
        { 
            if(ett.Id < 1)
            {
                throw new Exception("INVALID ID");
            } 

            if(String.IsNullOrEmpty(ett.Left) && String.IsNullOrEmpty(ett.Right))
            {
                throw new Exception("INVALID ENTITY - YOU MUST HAVE LEFT OR RIGHT");
            }
        }
    }
}