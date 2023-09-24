using System;
using Unity.Entities;
using Debug = System.Diagnostics.Debug;

namespace Week3
{
    public partial struct CrateEntity : ISystem
    {
        // Start is called before the first frame update
        void Start()
        {
            Console.WriteLine("Hi");
            Debug.WriteLine("Hi but in debug");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
