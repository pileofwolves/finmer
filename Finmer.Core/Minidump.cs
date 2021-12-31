/*
 * FINMER - Interactive Text Adventure
 * Copyright (C) 2019-2022 Nuntis the Wolf.
 *
 * Licensed under the GNU General Public License v3.0 (GPL3). See LICENSE.md for details.
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Finmer.Core
{

    /// <summary>
    /// Exposes native functions for writing a minidump.
    /// </summary>
    public static class Minidump
    {

        public enum EExceptionInfo
        {
            None,
            Present
        }

        [Flags]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S2346:Flags enumerations zero-value members should be named \"None\"",
            Justification = "Enumeration is part of Windows SDK and therefore should be used verbatim")]
        public enum EMinidumpOptions : uint
        {
            Normal = 0x00000000,
            WithDataSegs = 0x00000001,
            WithFullMemory = 0x00000002,
            WithHandleData = 0x00000004,
            FilterMemory = 0x00000008,
            ScanMemory = 0x00000010,
            WithUnloadedModules = 0x00000020,
            WithIndirectlyReferencedMemory = 0x00000040,
            FilterModulePaths = 0x00000080,
            WithProcessThreadData = 0x00000100,
            WithPrivateReadWriteMemory = 0x00000200,
            WithoutOptionalData = 0x00000400,
            WithFullMemoryInfo = 0x00000800,
            WithThreadInfo = 0x00001000,
            WithCodeSegs = 0x00002000,
            WithoutAuxiliaryState = 0x00004000,
            WithFullAuxiliaryState = 0x00008000,
            WithPrivateWriteCopyMemory = 0x00010000,
            IgnoreInaccessibleMemory = 0x00020000,
            ValidTypeFlags = 0x0003ffff
        }

        // Overload requiring MiniDumpExceptionInformation
        [DllImport("dbghelp", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, ref MiniDumpExceptionInformation expParam, IntPtr userStreamParam, IntPtr callbackParam);

        // Overload supporting MiniDumpExceptionInformation == NULL
        [DllImport("dbghelp", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

        [DllImport("kernel32", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
        private static extern uint GetCurrentThreadId();

        public static void Write(SafeHandle fileHandle, EMinidumpOptions options, EExceptionInfo exceptionInfo = EExceptionInfo.None)
        {
            // Obtain handle for the current process
            var current_process = Process.GetCurrentProcess();
            var current_process_handle = current_process.Handle;
            var current_process_id = (uint)current_process.Id;

            // Populate minidump parameters
            MiniDumpExceptionInformation exp;
            exp.ThreadId = GetCurrentThreadId();
            exp.ClientPointers = true;
            exp.ExceptionPointers = exceptionInfo == EExceptionInfo.Present ? Marshal.GetExceptionPointers() : IntPtr.Zero;

            // Dispatch the minidump call
            if (exp.ExceptionPointers == IntPtr.Zero)
                MiniDumpWriteDump(current_process_handle, current_process_id, fileHandle, (uint)options, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            else
                MiniDumpWriteDump(current_process_handle, current_process_id, fileHandle, (uint)options, ref exp, IntPtr.Zero, IntPtr.Zero);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S4487:Unread \"private\" fields should be removed", Justification = "Struct is part of Windows SDK and therefore must be used verbatim")]
        private struct MiniDumpExceptionInformation
        {
            public uint ThreadId;
            public IntPtr ExceptionPointers;
            [MarshalAs(UnmanagedType.Bool)] public bool ClientPointers;

        }

    }

}
