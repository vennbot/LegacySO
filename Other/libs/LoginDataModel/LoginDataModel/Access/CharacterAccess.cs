// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0.

/*
    Original Source: FreeSO (https://github.com/riperiperi/FreeSO)
    Original Author(s): The FreeSO Development Team

    Modifications for LegacySO by Benjamin Venn (https://github.com/vennbot):
    - Adjusted to support self-hosted LegacySO servers.
    - Modified to allow the LegacySO game client to connect to a predefined server by default.
    - Gameplay logic changes for a balanced and fair experience.
    - Updated references from "FreeSO" to "LegacySO" where appropriate.
    - Other changes documented in commit history and project README.

    Credit is retained for the original FreeSO project and its contributors.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginDataModel.Entities
{
    public class CharacterAccess
    {
        private DataAccess Context;

        public CharacterAccess(DataAccess context)
        {
            this.Context = context;
        }

        public IQueryable<Character> GetForAccount(int accountId)
        {
            return Context.Context.Characters.Where(x => x.AccountID == accountId);
        }

        public CharacterCreationStatus CreateCharacter(Character Char)
        {
            if (Char.Name.Length > 24)
            {
                return CharacterCreationStatus.NameTooLong;
            }

            try
            {
                Context.Context.Characters.InsertOnSubmit(Char);
                Context.Context.SubmitChanges();
            }
            catch (Exception E)
            {
                Logger.Log("Exception when creating character:\r\n" + E.ToString(), LogLevel.warn);
                return CharacterCreationStatus.NameAlreadyExisted;
            }

            return CharacterCreationStatus.Success;
        }

        public void RetireCharacter(Character Char)
        {
            Context.Context.Characters.DeleteOnSubmit(Char);
            Context.Context.SubmitChanges();
        }
    }

    public enum CharacterCreationStatus
    {
        NameAlreadyExisted,
        NameTooLong,
        ExceededCharacterLimit,
        Success,
        GeneralError
    }
}
