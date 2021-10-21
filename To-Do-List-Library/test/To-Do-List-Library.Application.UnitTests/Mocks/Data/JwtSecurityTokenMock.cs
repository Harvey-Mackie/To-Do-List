using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List_Library.Application.UnitTests.Mocks.Data
{
    public static class JwtSecurityTokenMock
    {
        public static Task<string> GetValidJwtSecurityToken()
        {
            var jwtSecurityToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI1OTBmZjYyMi1kNzEwLTRjZDQtOTdkYy1kN2M3MzUxOWRhNWQiLCJlbWFpbCI6InRlc3RAdGVzdC5jb20iLCJleHAiOjI1MzQwMjMwMDgwMH0.Q5Nvejc-2c9GviPhMlxSRvCaFdqnj3DxtgN_A6ZfsSg";
            return Task.Run(() => { return jwtSecurityToken; });
        }
    }
}
