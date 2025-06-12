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
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using FSO.Server.Common.Config;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FSO.Server.Api.Core.Services
{
    public class AWSUpdateUploader : IUpdateUploader
    {
        private AWSConfig Config;
        public AWSUpdateUploader(AWSConfig config)
        {
            Config = config;
        }

        public async Task<string> UploadFile(string destPath, string fileName, string groupName)
        {
            var region = Config.Region;
            var bucket = Config.Bucket;
            var s3config = new AmazonS3Config()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(region),
                Timeout = new TimeSpan(1, 0, 0),
                ReadWriteTimeout = new TimeSpan(1, 0, 0),
                MaxErrorRetry = 512
            };

            using (var aws = new AmazonS3Client(new BasicAWSCredentials(Config.AccessKeyID, Config.SecretAccessKey), s3config))
            {
                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite),
                    BucketName = bucket,
                    CannedACL = S3CannedACL.PublicRead,
                    Key = destPath
                };
                PutObjectResponse response = await aws.PutObjectAsync(request);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return $"https://s3.{region}.amazonaws.com/{bucket}/" + destPath;
                }
                else
                {
                    throw new Exception("Uploading file " + destPath + " failed with code " + response.HttpStatusCode + "!");
                }
            }
        }
    }
}
