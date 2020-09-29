find_package(alibabacloud_roautil ${alibabacloud_roautil_MINIMUM_REQUIRED} QUIET COMPONENTS ${alibabacloud_roautil_COMPONENTS_REQUIRED})

if(alibabacloud_roautil_FOUND)
  message(STATUS "Found alibabacloud_roautil version ${alibabacloud_roautil_MAJOR_VERSION}.${alibabacloud_roautil_MINOR_VERSION}.${alibabacloud_roautil_SUBMINOR_VERSION}")
  add_library(alibabacloud_roautil_external INTERFACE)
else()
  message(STATUS "alibabacloud_roautil ${alibabacloud_roautil_MINIMUM_REQUIRED} could not be located, Building alibabacloud_roautil instead.")
  include(FetchContent)

  FetchContent_Declare(
    _alibabacloud_roautil
    GIT_REPOSITORY https://github.com/alibabacloud-sdk-cpp/roautil.git
    GIT_TAG master)
  
  FetchContent_GetProperties(_alibabacloud_roautil)
  if(NOT _alibabacloud_roautil_POPULATED)
    FetchContent_Populate(_alibabacloud_roautil)
    message(STATUS "Added alibabacloud_roautil source : ${_alibabacloud_roautil_SOURCE_DIR}")
    message(STATUS "Added alibabacloud_roautil binary : ${_alibabacloud_roautil_BINARY_DIR}")
    add_subdirectory(
      ${_alibabacloud_roautil_SOURCE_DIR} 
      ${_alibabacloud_roautil_BINARY_DIR}
    )
    target_include_directories(alibabacloud_roa PRIVATE "${_alibabacloud_roautil_SOURCE_DIR}/include")
  endif()
endif()
