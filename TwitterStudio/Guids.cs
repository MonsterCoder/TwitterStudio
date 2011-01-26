// Guids.cs
// MUST match guids.h
using System;

namespace Company.TwitterStudio
{
    static class GuidList
    {
        public const string guidTwitterStudioPkgString = "3ec16d46-a7b5-4f14-9d8d-2853e7c6d342";
        public const string guidTwitterStudioCmdSetString = "59a1ad52-0f4d-4b78-bced-67bd69636e86";
        public const string guidToolWindowPersistanceString = "850f1f53-1e1e-4332-b65b-17a16ec9f430";

        public static readonly Guid guidTwitterStudioCmdSet = new Guid(guidTwitterStudioCmdSetString);
    };
}