/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 6/27/2011
 * Time: 4:41 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.IO;
namespace Refresh.FlashTools
{

    /// <summary>
    /// Read only container holding meta data embedded in FLV files
    /// </summary>
    public class FlvMetaInfo
    {
        private Double _duration;
        private Double _width;
        private Double _height;
        private Double _frameRate;
        private Double _videoDataRate;
        private Double _audioDataRate;
        private DateTime _creationDate;
        private bool _hasMetaData;
        /// <summary>
        /// The duration in seconds of the video
        /// </summary>
        public Double Duration
        {
            get { return _duration; }
            //set { _duration = value; }
        }
        /// <summary>
        /// The width in pixels of the video
        /// </summary>
        public Double Width
        {
            get { return _width; }
            //set { _width = value; }
        }
        /// <summary>
        /// The height in pixels of the video
        /// </summary>
        public Double Height
        {
            get { return _height; }
            //set { _height = value; }
        }
        /// <summary>
        /// The data rate in KB/sec of the video 
        /// </summary>
        public Double VideoDataRate
        {
            get { return _videoDataRate; }
            //set { _videoDataRate = value; }
        }
        /// <summary>
        /// The data rate in KB/sec of the video's audio track
        /// </summary>
        public Double AudioDataRate
        {
            get { return _audioDataRate; }
            //set { _audioDataRate = value; }
        }
        /// <summary>
        /// The frame rate of the video
        /// </summary>
        public Double FrameRate
        {
            get { return _frameRate; }
            //set { _frameRate = value; }
        }
        /// <summary>
        /// The creation date of the video
        /// </summary>
        public DateTime CreationDate
        {
            get { return _creationDate; }
            //set { _creationDate = value; }
        }
        /// <summary>
        /// Whether or not the FLV has meta data
        /// </summary>
        public bool HasMetaData
        {
            get { return _hasMetaData; }
            //set { _hasMetaData = value; }
        }
        internal FlvMetaInfo(bool hasMetaData, Double duration, Double width, Double height, Double videoDataRate, Double audioDataRate, Double frameRate, DateTime creationDate)
        {
            _hasMetaData = hasMetaData;
            _duration = duration;
            _width = width;
            _height = height;
            _videoDataRate = videoDataRate;
            _audioDataRate = audioDataRate;
            _frameRate = frameRate;
            _creationDate = creationDate;
        }
    }
}
