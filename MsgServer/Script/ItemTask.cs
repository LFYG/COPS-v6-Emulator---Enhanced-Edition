﻿// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using MoonSharp.Interpreter;

namespace COServer.Script
{
    public class ItemTask : Task
    {
        /// <summary>
        /// The logger of the class.
        /// </summary>
        private static readonly log4net.ILog sLogger = log4net.LogManager.GetLogger(typeof(ItemTask));

        /// <summary>
        /// The unique ID of the task.
        /// </summary>
        private readonly Int32 mId;

        /// <summary>
        /// Create a new item task using the specified Lua script.
        /// </summary>
        /// <param name="aId">The unique ID of the task</param>
        /// <param name="aPath">The path of the Lua script</param>
        public ItemTask(Int32 aId, String aPath)
            : base(aPath, String.Format("useItem{0}", aId))
        {
            mId = aId;
        }

        /// <summary>
        /// Execute the task using the specified arguments.
        /// </summary>
        /// <param name="aClient">The client executing the task</param>
        /// <param name="aArgs">The arguments of the task</param>
        public override void Execute(Client aClient, params object[] aArgs)
        {
            Item item = (Item)aArgs[0];

            sLogger.Info("Executing item task {0} with self={2}, client={1}",
                mId, aClient, item.Id);

            try
            {
                sLogger.Debug("Calling function {0}", mFct);

                var env = GetEnvironment();
                var res = env.Call(env.Globals[mFct], item, aClient);
            }
            catch (ScriptRuntimeException exc)
            {
                sLogger.Error("Moon# returned an error while calling {0}.\n{1}",
                    mFct, exc.Message);
            }
        }
    }
}
