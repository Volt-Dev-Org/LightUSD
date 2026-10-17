using System.Linq;

using Sharpmake;

namespace VoltSharpmake
{
	// Builds LightUSD (https://github.com/lighttransport/LightUSD) as a static library.
	//
	// The source list mirrors the CMake targets lightusd_static and lightusd_textools,
	// configured with:
	//   LIGHTUSD_BUILD_TESTS/EXAMPLES/TOOLS/BENCHMARKS=OFF, LIGHTUSD_WITH_C_API=OFF,
	//   LIGHTUSD_WITH_USD_TO_GLTF=OFF, LIGHTUSD_WITH_AUDIO=OFF, everything else default.
	// When updating LightUSD, re-check these lists against the CMakeLists.txt
	// (configure with Ninja and look at the lightusd_static / lightusd_textools
	// rules in build.ninja).
	//
	// LightUSD uses neither exceptions nor RTTI, so it is built with Volt's default
	// settings and uses the operator new of whichever module it is linked into.
	[Sharpmake.Generate]
	public class LightUSD : CommonThirdPartyLibProject
	{
		// Relative to src/.
		private static readonly string[] s_coreSources =
		{
			"arg-parser.cc",
			"ascii-parser-basetype-inst.cc",
			"ascii-parser-basetype.cc",
			"ascii-parser-entry.cc",
			"ascii-parser-props.cc",
			"ascii-parser-timesamples-array.cc",
			"ascii-parser-timesamples.cc",
			"ascii-parser.cc",
			"asset-resolution.cc",
			"audio-loader.cc",
			"base122.cc",
			"chunk-reader.cc",
			"color-space.cc",
			"color-transform.cc",
			"composition-graph.cc",
			"composition-reconstruct.cc",
			"composition.cc",
			"core/instance-key.cc",
			"core/path-expression-eval.cc",
			"core/path-expression.cc",
			"core/prim-enums.cc",
			"crate-dump.cc",
			"crate-format.cc",
			"crate-path-utils/path_sort.cc",
			"crate-path-utils/tree_encode.cc",
			"crate-pprint.cc",
			"crate-reader-arrays.cc",
			"crate-reader-paths.cc",
			"crate-reader-timesamples.cc",
			"crate-reader-values.cc",
			"crate-reader.cc",
			"crate-writer-inline.cc",
			"crate-writer-values.cc",
			"crate-writer.cc",
			"enum-handlers.cc",
			"external/fpng.cpp",
			"external/mikktspace/mikktspace.c",
			"external/miniz.c",
			"external/nanoimage/nanoimage_alloc.c",
			"external/nanoimage/nanoimage_bmp.c",
			"external/nanoimage/nanoimage_jpeg.c",
			"external/nanoimage/nanoimage_png.c",
			"external/nanoimage/nanoimage_tga.c",
			"external/nanoimage/nanoimage_zlib.c",
			"external/pugixml.cpp",
			"external/tiny_dng_loader.cc",
			"external/tiny_obj_loader.cc",
			"external/tinyexr/src/exr_attr.c",
			"external/tinyexr/src/exr_b44.c",
			"external/tinyexr/src/exr_codec.c",
			"external/tinyexr/src/exr_color.c",
			"external/tinyexr/src/exr_context.c",
			"external/tinyexr/src/exr_convert.c",
			"external/tinyexr/src/exr_core.c",
			"external/tinyexr/src/exr_cpu.c",
			"external/tinyexr/src/exr_deep.c",
			"external/tinyexr/src/exr_deflate.c",
			"external/tinyexr/src/exr_fpnge.c",
			"external/tinyexr/src/exr_freestanding.c",
			"external/tinyexr/src/exr_half.c",
			"external/tinyexr/src/exr_mip.c",
			"external/tinyexr/src/exr_piz.c",
			"external/tinyexr/src/exr_pxr24.c",
			"external/tinyexr/src/exr_reader.c",
			"external/tinyexr/src/exr_resize.c",
			"external/tinyexr/src/exr_rle.c",
			"external/tinyexr/src/exr_simd_neon.c",
			"external/tinyexr/src/exr_simd_x86.c",
			"external/tinyexr/src/exr_spectral.c",
			"external/tinyexr/src/exr_stdio.c",
			"external/tinyexr/src/exr_thread.c",
			"external/tinyexr/src/exr_tonemap.c",
			"external/tinyexr/src/exr_util_simd_neon.c",
			"external/tinyexr/src/exr_util_simd_x86.c",
			"external/tinyexr/src/exr_writer.c",
			"external/tinyexr/src/exr_zip.c",
			"external/tinyvdb/tinyvdb_io.c",
			"external/yyjson.c",
			"external/zstd.c",
			"hash-util.cc",
			"image-loader.cc",
			"image-util.cc",
			"image-writer.cc",
			"imageio/png-stream.cc",
			"imageproc/simd.cc",
			"integerCoding.cpp",
			"io-util.cc",
			"json-to-usd.cc",
			"json-util.cc",
			"json-writer.cc",
			"large-scene-loader.cc",
			"layer.cc",
			"lightusd.cc",
			"linear-algebra.cc",
			"lz4-compression.cc",
			"lz4/lz4.c",
			"minijson.cc",
			"mjcPhysics.cc",
			"mtlx-dom.cc",
			"mtlx-simple-parser.cc",
			"mtlx-xml-parser.cc",
			"mtlx-xml-tokenizer.cc",
			"parser-timing.cc",
			"path-util.cc",
			"pcp/cache-parallel.cc",
			"pcp/cache.cc",
			"pcp/layer-registry.cc",
			"performance.cc",
			"pprint-ar.cc",
			"pprint-enum.cc",
			"pprint-geom.cc",
			"pprint-light.cc",
			"pprint-media.cc",
			"pprint-meta.cc",
			"pprint-physics.cc",
			"pprint-shader.cc",
			"pprint-skel.cc",
			"pprinter.cc",
			"prim-composition.cc",
			"prim-pprint-parallel.cc",
			"prim-reconstruct-ar.cc",
			"prim-reconstruct-geom.cc",
			"prim-reconstruct-geom2.cc",
			"prim-reconstruct-geom3.cc",
			"prim-reconstruct-lightprim.cc",
			"prim-reconstruct-media.cc",
			"prim-reconstruct-physics.cc",
			"prim-reconstruct-physics2.cc",
			"prim-reconstruct-physics3.cc",
			"prim-reconstruct-physics4.cc",
			"prim-reconstruct-shader.cc",
			"prim-reconstruct-shader2.cc",
			"prim-reconstruct-shader3.cc",
			"prim-reconstruct-shader4.cc",
			"prim-reconstruct-vol.cc",
			"prim-reconstruct.cc",
			"prim-reconstruct2.cc",
			"prim-reconstruct3.cc",
			"prim-types-schema.cc",
			"prim-types.cc",
			"primvar.cc",
			"ptx-loader.cc",
			"pxr-compat.cc",
			"sconv-ar.cc",
			"sconv-geom.cc",
			"sconv-layer.cc",
			"sconv-light.cc",
			"sconv-media.cc",
			"sconv-physics.cc",
			"sconv-shader.cc",
			"sconv-skel.cc",
			"sha256.cc",
			"spline-binary.cc",
			"stage-converter.cc",
			"stage.cc",
			"str-util.cc",
			"task-queue.cc",
			"timesamples-eval.cc",
			"timesamples-pprint.cc",
			"timesamples.cc",
			"tiny-format.cc",
			"tiny-string.cc",
			"tsd/tsd-bilinear.cc",
			"tsd/tsd-catmark.cc",
			"tsd/tsd-fvar.cc",
			"tsd/tsd-lightusd.cc",
			"tsd/tsd-limit.cc",
			"tsd/tsd-loop.cc",
			"tsd/tsd-refine.cc",
			"tsd/tsd-stream.cc",
			"tsd/tsd-topology.cc",
			"tsd/tsd-validate.cc",
			"tydra/ar-to-json.cc",
			"tydra/attribute-eval-typed-all.cc",
			"tydra/attribute-eval-typed-inst-array.cc",
			"tydra/attribute-eval-typed-inst-scalar.cc",
			"tydra/attribute-eval.cc",
			"tydra/bone-util.cc",
			"tydra/color-management.cc",
			"tydra/command-and-history.cc",
			"tydra/common-utils.cc",
			"tydra/diff-and-compare.cc",
			"tydra/facial.cc",
			"tydra/ik-solver.cc",
			"tydra/js-script-persistent.cc",
			"tydra/js-script.cc",
			"tydra/layer-to-renderscene.cc",
			"tydra/material-serializer.cc",
			"tydra/materialx-to-json.cc",
			"tydra/mcp-js-bridge.cc",
			"tydra/mcp-resources.cc",
			"tydra/mcp-server.cc",
			"tydra/mcp-tools-composition.cc",
			"tydra/mcp-tools-diff.cc",
			"tydra/mcp-tools-query.cc",
			"tydra/mcp-tools-scene.cc",
			"tydra/mcp-tools-usdz.cc",
			"tydra/mcp-tools-validate.cc",
			"tydra/mcp-tools.cc",
			"tydra/mcp.cc",
			"tydra/obj-export.cc",
			"tydra/physics-to-json.cc",
			"tydra/prim-apply.cc",
			"tydra/raytracing-data.cc",
			"tydra/raytracing-scene-converter.cc",
			"tydra/rb-collision.cc",
			"tydra/rb-dynamics.cc",
			"tydra/render-data-anim.cc",
			"tydra/render-data-instancer.cc",
			"tydra/render-data-material-mtlx.cc",
			"tydra/render-data-material.cc",
			"tydra/render-data-mesh-tangent.cc",
			"tydra/render-data-mesh.cc",
			"tydra/render-data-pprint.cc",
			"tydra/render-data.cc",
			"tydra/render-scene-dump.cc",
			"tydra/scene-access-listprims-inst.cc",
			"tydra/scene-access-listshaders-inst.cc",
			"tydra/scene-access.cc",
			"tydra/scene-analysis.cc",
			"tydra/shader-network.cc",
			"tydra/texture-util.cc",
			"tydra/threejs-exporter.cc",
			"tydra/urdf-to-usd.cc",
			"tydra/usd-export.cc",
			"tydra/value-to-json.cc",
			"tydra/variant-applier.cc",
			"tydra/variant-converter.cc",
			"tydra/variant-support.cc",
			"typed-array.cc",
			"usd-dump.cc",
			"usd-to-json.cc",
			"usd-validation.cc",
			"usdGeom-accessors.cc",
			"usdGeom-primvar-inst-role-a.cc",
			"usdGeom-primvar-inst-role-b.cc",
			"usdGeom-primvar-inst-scalar-a.cc",
			"usdGeom-primvar-inst-scalar-b.cc",
			"usdGeom-primvar-inst-vec-a.cc",
			"usdGeom-primvar-inst-vec-b.cc",
			"usdGeom.cc",
			"usdLux.cc",
			"usdMtlx-write.cc",
			"usdMtlx.cc",
			"usdObj.cc",
			"usdPhysics.cc",
			"usdShade.cc",
			"usdSkel.cc",
			"usdVol.cc",
			"usdVox.cc",
			"usda-reader-reconstruct-1.cc",
			"usda-reader-reconstruct-2.cc",
			"usda-reader-reconstruct-3.cc",
			"usda-reader-reconstruct-4.cc",
			"usda-reader.cc",
			"usda-writer.cc",
			"usdc-reader-prim-parse.cc",
			"usdc-reader-prim-reconstruct-1.cc",
			"usdc-reader-prim-reconstruct-2.cc",
			"usdc-reader-prim-reconstruct-3.cc",
			"usdc-reader-prim-reconstruct-4.cc",
			"usdc-reader-prim.cc",
			"usdc-reader-property.cc",
			"usdc-reader-reconstruct.cc",
			"usdc-reader.cc",
			"usdc-writer.cc",
			"usdz-convert.cc",
			"usdz-geometry-optimize.cc",
			"usdz-material-optimize.cc",
			"uuid-gen.cc",
			"value-eval-util.cc",
			"value-pprint-dispatch.cc",
			"value-pprint-prim.cc",
			"value-pprint.cc",
			"value-types-cast.cc",
			"value-types-lerp.cc",
			"value-types-matrix.cc",
			"value-types-typename.cc",
			"value-types.cc",
			"xform.cc",
			"zstd-compression.cc"
		};

		private static readonly string[] s_textoolsSources =
		{
			"external/textools/envmap/src/envmap_ibl.c",
			"external/textools/envmap/src/envmap_proj.c",
			"external/textools/envmap/src/envmap_sample.c",
			"external/textools/envmap/src/envmap_sg.c",
			"external/textools/envmap/src/envmap_sh.c",
			"external/textools/resize/src/tir_convert.c",
			"external/textools/resize/src/tir_core.c",
			"external/textools/resize/src/tir_cpu.c",
			"external/textools/resize/src/tir_domain.c",
			"external/textools/resize/src/tir_kernels_neon.c",
			"external/textools/resize/src/tir_kernels_scalar.c",
			"external/textools/resize/src/tir_kernels_sve.c",
			"external/textools/resize/src/tir_kernels_x86.c",
			"external/textools/resize/src/tir_pipeline.c",
			"external/textools/resize/src/tir_table.c",
			"external/textools/resize/src/tir_thread.c",
			"external/textools/texcomp/src/texcomp.c",
			"external/textools/texcomp/src/texcomp_astc.c",
			"external/textools/texcomp/src/texcomp_astc_decode.c",
			"external/textools/texcomp/src/texcomp_astc_hdr.c",
			"external/textools/texcomp/src/texcomp_bc1.c",
			"external/textools/texcomp/src/texcomp_bc3.c",
			"external/textools/texcomp/src/texcomp_bc5.c",
			"external/textools/texcomp/src/texcomp_bc6h.c",
			"external/textools/texcomp/src/texcomp_bc6h_decode.c",
			"external/textools/texcomp/src/texcomp_bc7.c",
			"external/textools/texcomp/src/texcomp_eac.c",
			"external/textools/texcomp/src/texcomp_etc2.c",
			"external/textools/texcomp/src/texcomp_etc2_decode.c",
			"external/textools/texcomp/src/texcomp_uni.c",
			"external/textools/texpipe/src/texpipe.c",
			"external/textools/texpipe/src/texpipe_alpha.c",
			"external/textools/texpipe/src/texpipe_container.c",
			"external/textools/texpipe/src/texpipe_cube.c",
			"external/textools/texpipe/src/texpipe_disp.c",
			"external/textools/texpipe/src/texpipe_mip.c",
			"external/textools/texpipe/src/texpipe_normal.c",
			"external/textools/texpipe/src/texpipe_octa.c"
		};

		private static readonly string[] s_textoolsIncludeDirectories =
		{
			"external/textools/resize/include",
			"external/textools/texcomp/include",
			"external/textools/texpipe/include",
			"external/textools/envmap/include",
		};

		// Feature defines. Several headers depend on these, so they are also
		// exported to everything that includes LightUSD.
		private static readonly string[] s_publicDefines =
		{
			"LIGHTUSD_CXX_EXCEPTIONS=0",
			"LIGHTUSD_CXX_RTTI=0",
			"JSON_NOEXCEPTION=1",
			"LIGHTUSD_ENABLE_NLOHMANN_JSON_COMPAT",
			"LIGHTUSD_EXR_V3=1",
			"LIGHTUSD_HAVE_FPNG",
			"LIGHTUSD_USE_USDMTLX",
			"LIGHTUSD_USE_USDOBJ",
			"LIGHTUSD_USE_USDVOL",
			"LIGHTUSD_USE_USDVOX",
			"LIGHTUSD_WITH_COLORIO",
			"LIGHTUSD_WITH_EXR",
			"LIGHTUSD_WITH_JSON",
			"LIGHTUSD_WITH_NANOIMAGE",
			"LIGHTUSD_WITH_PCP",
			"LIGHTUSD_WITH_TEXTOOLS",
			"LIGHTUSD_WITH_TIFF",
			"LIGHTUSD_WITH_TYDRA",
			"LIGHTUSD_WITH_ZSTD_COMPRESSION",

			// LightUSD is C++17 while Volt is compiled with the latest standard, where the
			// bundled polyfills (nonstd/*.hpp) would otherwise switch to the std types and
			// no longer match the library.
			"nsel_CONFIG_SELECT_EXPECTED=1", // nsel_EXPECTED_NONSTD
			"nssv_CONFIG_SELECT_STRING_VIEW=2", // nssv_STRING_VIEW_STD
		};

		// Only used by individual vendored libraries (tinyexr, fpng, zstd, nanoimage...)
		// in CMake, they are prefixed per library so applying them to all files is harmless.
		private static readonly string[] s_privateDefines =
		{
			"EXR_NO_JPH",
			"EXR_NO_ZSTD",
			"FPNG_NO_SSE=1",
			"TINY_DNG_LOADER_ENABLE_ZIP=1",
			"ZSTD_DISABLE_ASM=1",
			"NI_USE_MINIZ",
			"_LARGEFILE64_SOURCE=1",
			"_ALLOW_COMPILER_AND_STL_VERSION_MISMATCH",
		};

		public LightUSD() : base()
		{
			AddTargets(CommonTarget.GetDefaultTargets());
			Name = "LightUSD";

			SourceRootPath = @"[project.RootPath]/[project.Name]/src";

			// Only compile the files CMake would, the repository also contains tests, tools,
			// optional modules and the experimental "next" rewrite. The filters are relative
			// to this file, not to SourceRootPath.
			SourceFilesFilters = new Strings(s_coreSources.Concat(s_textoolsSources).Select(source => "src/" + source));
		}

		public override void ConfigureAll(Configuration conf, CommonTarget target)
		{
			base.ConfigureAll(conf, target);

			conf.IsExcludedFromBuild = true;

			// LightUSD headers are included relative to src, e.g. <lightusd.hh> and <tydra/render-data.hh>.
			conf.IncludePaths.Add(@"[project.SourceRootPath]");
			foreach (string includeDirectory in s_textoolsIncludeDirectories)
			{
				conf.IncludePaths.Add(@"[project.SourceRootPath]/" + includeDirectory);
			}

			conf.IncludePrivatePaths.Add(@"[project.SourceRootPath]/external");
			conf.IncludePrivatePaths.Add(@"[project.SourceRootPath]/external/tinyexr/include");
			conf.IncludePrivatePaths.Add(@"[project.SourceRootPath]/external/tinyexr/src");

			foreach (string define in s_publicDefines)
			{
				conf.Defines.Add(define);
				conf.ExportDefines.Add(define);
			}

			conf.Defines.Add(s_privateDefines);

			// Matches LightUSD's CMake configuration.
			conf.Options.Add(Options.Vc.Compiler.CppLanguageStandard.CPP17);
			conf.Options.Add(Options.Vc.Compiler.CLanguageStandard.C11);
			conf.AdditionalCompilerOptions.Add("/bigobj");
		}
	}
}
