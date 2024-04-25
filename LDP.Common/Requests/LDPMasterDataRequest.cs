﻿using LDP_APIs.Models;

namespace LDP_APIs.BL.APIRequests
{
    public class LDPMasterDataRequest
    {
        public string?  MaserDataType { get; set; }
    }

    public class LDPMasterDataByMultipleTypesRequest
    {
        public List<string>? MaserDataTypes { get; set; }


    }
}
