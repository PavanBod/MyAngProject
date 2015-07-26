﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIT.Common
{
    public static class CITConstants
    {
        public static readonly byte[] CRYPTO_AES_KEY = { 0x00, 0x01, 0x03, 0x05, 0x07, 0x09, 0x01, 0x05, 0x05, 0x05, 0x0A, 0x0C, 0x0E, 0x0A, 0x0C, 0x0E };
        public static readonly byte[] CRYPTI_AES_IV = { 0x00, 0x02, 0x04, 0x06, 0x08, 0x02, 0x04, 0x06, 0x08, 0x02, 0x0B, 0x0D, 0x0F, 0x0B, 0x0D, 0x0F };
    }
}