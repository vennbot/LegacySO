
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
using FSO.Common.DatabaseService.Framework;
using FSO.Common.DataService.Framework;
using FSO.Common.Serialization;
using FSO.Server.Protocol.Voltron.DataService;
using Ninject.Activation;
using Ninject.Modules;
using System;

namespace FSO.Server.DataService
{
    /// <summary>
    /// Data service classes that can be shared between multiple shards when multi-tenanting
    /// </summary>
    public class GlobalDataServiceModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<cTSOSerializer>().ToProvider<cTSOSerializerProvider>().InSingletonScope();
            this.Bind<IModelSerializer>().ToProvider<ModelSerializerProvider>().InSingletonScope();
            this.Bind<ISerializationContext>().To<SerializationContext>();
        }
    }

    class ModelSerializerProvider : IProvider<IModelSerializer>
    {
        private Content.Content Content;

        public ModelSerializerProvider(Content.Content content)
        {
            this.Content = content;
        }

        public Type Type
        {
            get
            {
                return typeof(IModelSerializer);
            }
        }

        public object Create(IContext context)
        {
            var serializer = new ModelSerializer();
            serializer.AddTypeSerializer(new DatabaseTypeSerializer());
            serializer.AddTypeSerializer(new DataServiceModelTypeSerializer(Content.DataDefinition));
            serializer.AddTypeSerializer(new DataServiceModelVectorTypeSerializer(Content.DataDefinition));
            return serializer;
        }
    }

    class cTSOSerializerProvider : IProvider<cTSOSerializer>
    {
        private Content.Content Content;

        public cTSOSerializerProvider(Content.Content content)
        {
            this.Content = content;
        }

        public Type Type
        {
            get
            {
                return typeof(cTSOSerializer);
            }
        }

        public object Create(IContext context){
            return new cTSOSerializer(this.Content.DataDefinition);
        }
    }
}
