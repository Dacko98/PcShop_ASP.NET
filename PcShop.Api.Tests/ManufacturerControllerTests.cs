/* File:        ManufacturerControllerTests.cs
 * 
 * Solution:    PcShop
 * Project:     PcShop.Api.Test
 *
 * Team:        Team0011
 * Author:      Vojtech Vlach
 * Login:       xvlach22
 * Date:        30.10.2020
 * 
 * Description: This file contains API tests for ManufacturerController in PcShop.Api.
 *              Tests all main 4 methods (GET, PUT, POST, DELETE)
 * 
 * Installed NuGet packages: Microsoft.AspNetCore.Mvc.Testing, FluentAssertions
 */

using Microsoft.AspNetCore.Mvc.Testing;
using PcShop.BL.Api.Models.Category;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using FluentAssertions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System;
using Xunit;

namespace PcShop.Api.Tests
{
    [Collection(name: "ManufacturerControllerTests")]
    public class ManufacturerControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        public ManufacturerControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public void HelloTest()
        {
            // Empty test to be deleted
            Debug.Print("Hello test - ManufacturerController");
        }

        /*===============================    GetAll Tests    ===============================*/

        /*===============================    GetById Tests    ===============================*/

        /*===============================    Create Tests    ===============================*/

        /*===============================    Update Tests    ===============================*/

        /*===============================    Delete Tests    ===============================*/
    }
}
