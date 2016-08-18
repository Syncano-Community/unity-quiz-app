using UnityEngine;
using System.Collections;

/// <summary>
/// Wrapper class for getting response from Syncano.
/// </summary>
public class Response<T> : JsonData<T> where T : SyncanoObject<T>, new()
{
	/// <summary>
	/// Everything is ok!
	/// </summary>
	public const int HTTP_CODE_SUCCESS = 200;

	/// <summary>
	/// The request was successful and a resource was created.
	/// </summary>
	public const int HTTP_CODE_CREATED = 201;

	///<summary>
	/// The request was successful but there is no data to
	/// return (usually after successful DELETE request).
	///</summary>
	public const int HTTP_CODE_NO_CONTENT = 204;

	///<summary>
	/// There was no new data to return.
	///</summary>
	public const int HTTP_CODE_NOT_MODIFIED = 304;

	///<summary>
	/// The request was invalid or cannot be otherwise served.
	/// An accompanying error message will explain further.
	///</summary>
	public const int HTTP_CODE_BAD_REUEST = 400;

	///<summary>
	/// Authentication credentials were missing or incorrect.
	///</summary>
	public const int HTTP_CODE_UNAUTHORIZED = 401;

	///<summary>
	/// The request is understood, but it has been refused or
	/// access is not allowed. An accompanying error message will explain why.
	///</summary>
	public const int HTTP_CODE_FORBIDDEN = 403;

	///<summary>
	/// The URI requested is invalid or the resource requested, such as a user, does not exists.
	/// Also returned when the requested format is not supported by the requested method.
	///</summary>
	public const int HTTP_CODE_NOT_FOUND = 404;

	///<summary>
	/// Requested method is not supported for this resource.
	///</summary>
	public const int HTTP_CODE_METHOD_NOT_ALLOWED = 405;

	///<summary>
	/// Something is broken. Please contact Syncano support.
	///</summary>
	public const int HTTP_CODE_INTERNAL_SERVER_ERROR = 500;

	///<summary>
	/// Syncano is down. Contact support.
	///</summary>
	public const int HTTP_CODE_BAD_GATEWAY = 502;

	///<summary>
	/// Syncano request timeout.
	///</summary>
	public const int HTTP_CODE_GATEWAY_TIMEOUT = 504;

	///<summary>
	/// Status code when response is ok.
	///</summary>
	public const int CODE_SUCCESS = 0;

	///<summary>
	/// Status code when Http error appeared.
	///</summary>
	public const int CODE_HTTP_ERROR = 1;

	///<summary>
	/// Client Protocol Exception
	///</summary>
	public const int CODE_CLIENT_PROTOCOL_EXCEPTION = 2;

	///<summary>
	/// Illegal State Exception.
	///</summary>
	public const int CODE_ILLEGAL_STATE_EXCEPTION = 3;

	///<summary>
	/// IOException.
	///</summary>
	public const int CODE_ILLEGAL_IO_EXCEPTION = 4;

	///<summary>
	/// Parsing exception.
	///</summary>
	public const int CODE_PARSING_RESPONSE_EXCEPTION = 5;

	///<summary>
	/// Unknown
	///</summary>
	public const int CODE_UNKNOWN_ERROR = 6;

	/// <summary>
	/// The response status. Returns success or fail string.
	/// </summary>
	public string status;

	/// <summary>
	/// The duration of processing request.
	/// </summary>
	public int duration;

	/// <summary>
	/// The web response code.
	/// </summary>
	[System.NonSerialized]
	public long responseCode;

	/// <summary>
	/// The web error.
	/// </summary>
	[System.NonSerialized]
	public string webError;

	/// <summary>
	/// Gets or sets a value indicating whether request to Syncano was successful or not.
	/// </summary>
	/// <value><c>true</c> if is success; otherwise, <c>false</c>.</value>
	public bool IsSuccess { get; set; }

	/// <summary>
	/// Deserialized data.
	/// </summary>
	public T Data { set; get; }
}